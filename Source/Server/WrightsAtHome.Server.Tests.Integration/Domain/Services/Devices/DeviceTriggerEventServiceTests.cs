using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Devices.Internal;
using WrightsAtHome.Server.Domain.Services.Trigger;
using WrightsAtHome.Server.Tests.Integration.Utility;
using WrightsAtHome.Server.Tests.Unit.Utility;
using Xunit;

namespace WrightsAtHome.Server.Tests.Integration.Domain.Services.Devices
{
    [Collection("DatabaseCollection")]
    public class DeviceTriggerEventServiceTests
    {
        [Theory]
        [InlineData("AT 5:00pm", "at 11:00pm", "4:59pm", "Off", "Turn On AT 5:00pm")]
        [InlineData("AT 5:00pm", "at 11:00pm", "5:05pm", "Off", "Turn On AT 5:00pm tomorrow")]
        [InlineData("AT 5:00pm", "at 11:00pm", "5:01pm", "On", "Turn Off AT 11:00pm")]
        public async void AtTriggerTests(string trigger1, string trigger2, string currentTime, string stateName, string expectedTrigger)
        {
            using (var ctx = new AtHomeDbContext())
            {
                var device = DeviceUtils.CreateTestDevice(ctx, expectedTrigger, trigger1, trigger2);
                var helpers = new TriggerHelpersFake { CurrentTime = currentTime };
                var triggerComp = new DeviceTriggerCompiler(new TriggerCompiler(helpers));
                var stateSvc = new Mock<IDeviceStateService>();

                var currentState = ctx.DeviceStates.Single(s => s.Name == stateName);

                stateSvc.Setup(s => s.GetCurrentDeviceState(device.Id)).Returns(currentState);
                ctx.SaveChanges();

                var underTest = new DeviceTriggerEventService(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                string eventDesc = await underTest.GetNextTriggerEvent(device.Id);

                // Assert
                Assert.Equal(expectedTrigger, eventDesc);
            }
            
        }

        [Theory]
        [InlineData("When AirTemp > 80", "when AirTemp < 75", "4:59pm", "Off", "Turn On WHEN AirTemp > 80")]
        [InlineData("When AirTemp > 80", "when AirTemp < 75", "4:59pm", "On", "Turn Off WHEN AirTemp < 75")]
        [InlineData("When AirTemp > 80", "after 4 hours", "4:59pm", "On", "None")]
        public async void WhenTriggerTests(string trigger1, string trigger2, string currentTime, string stateName, string expectedTrigger)
        {
            using (var ctx = new AtHomeDbContext())
            {
                var device = DeviceUtils.CreateTestDevice(ctx, expectedTrigger, trigger1, trigger2);
                var helpers = new TriggerHelpersFake { CurrentTime = currentTime, SensorReadings = new Dictionary<string, decimal> {{ "AirTemp", 81m }}};
                var triggerComp = new DeviceTriggerCompiler(new TriggerCompiler(helpers));
                var stateSvc = new Mock<IDeviceStateService>();

                var currentState = ctx.DeviceStates.Single(s => s.Name == stateName);

                stateSvc.Setup(s => s.GetCurrentDeviceState(device.Id)).Returns(currentState);
                ctx.SaveChanges();

                var underTest = new DeviceTriggerEventService(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                string eventDesc = await underTest.GetNextTriggerEvent(device.Id);

                // Assert
                Assert.Equal(expectedTrigger, eventDesc);
            }
        }

        [Fact]
        public async void SimpleAfterTrigger()
        {
            using (var ctx = new AtHomeDbContext())
            {
                var device = DeviceUtils.CreateTestDevice(ctx, "test", "When AirTemp > 80", "AFTER 3 HOURS");
                var helpers = new TriggerHelpersFake { CurrentTime = "4:59pm", SensorReadings = new Dictionary<string, decimal> { { "AirTemp", 81m } } };
                var triggerComp = new DeviceTriggerCompiler(new TriggerCompiler(helpers));
                var stateSvc = new Mock<IDeviceStateService>();
                var wait = new DeviceTriggerWait
                {
                    AfterTrigger = device.Triggers[1],
                    StartTime = DateTime.Now.Date + new TimeSpan(17, 23, 0)
                };
                ctx.DeviceTriggerWaits.Add(wait);

                var currentState = ctx.DeviceStates.Single(s => s.Name == "On");

                stateSvc.Setup(s => s.GetCurrentDeviceState(device.Id)).Returns(currentState);
                ctx.SaveChanges();

                var underTest = new DeviceTriggerEventService(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                string eventDesc = await underTest.GetNextTriggerEvent(device.Id);

                // Assert
                Assert.Equal("Turn Off AFTER 3 hours [will happen at 8:23 PM]", eventDesc);
            }
        }

        [Fact]
        public async void TwoWaitingAfterTriggers()
        {
            using (var ctx = new AtHomeDbContext())
            {
                var device = DeviceUtils.CreateTestDevice(ctx, "test2", "When AirTemp > 80", "AFTER 3 HOURS");

                var offState = ctx.DeviceStates.Single(s => s.Name == "Off");

                device.Triggers.Add(new DeviceTrigger
                {
                   Device = device,
                   IsActive = true,
                   Sequence = 3,
                   ToState = offState,
                   TriggerText = "AFTER 2 HOURS"
                });

                var helpers = new TriggerHelpersFake { CurrentTime = "4:59pm", SensorReadings = new Dictionary<string, decimal> { { "AirTemp", 81m } } };
                var triggerComp = new DeviceTriggerCompiler(new TriggerCompiler(helpers));
                var stateSvc = new Mock<IDeviceStateService>();
                var wait = new DeviceTriggerWait
                {
                    AfterTrigger = device.Triggers[1],
                    StartTime = DateTime.Now.Date + new TimeSpan(17, 23, 0)
                };
                ctx.DeviceTriggerWaits.Add(wait);

                wait = new DeviceTriggerWait
                {
                    AfterTrigger = device.Triggers[2],
                    StartTime = DateTime.Now.Date + new TimeSpan(18, 22, 0)
                };
                ctx.DeviceTriggerWaits.Add(wait);

                var currentState = ctx.DeviceStates.Single(s => s.Name == "On");

                stateSvc.Setup(s => s.GetCurrentDeviceState(device.Id)).Returns(currentState);
                ctx.SaveChanges();

                var underTest = new DeviceTriggerEventService(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                string eventDesc = await underTest.GetNextTriggerEvent(device.Id);

                // Assert
                Assert.Equal("Turn Off AFTER 2 hours [will happen at 8:22 PM]", eventDesc);
            }
        }
    }
}
