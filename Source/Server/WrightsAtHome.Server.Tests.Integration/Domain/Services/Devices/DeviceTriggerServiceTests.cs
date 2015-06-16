using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services;
using WrightsAtHome.Server.Domain.Services.Devices;
using WrightsAtHome.Server.Domain.Services.Trigger;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser;
using WrightsAtHome.Server.Tests.Integration.Utility;
using WrightsAtHome.Tests.Integration.Utility;
using Xunit;

namespace WrightsAtHome.Tests.Integration.Domain.Services.Devices
{
    class FakeHelpers : ITriggerHelpers, IDateTimeHelpers
    {
        public string CurrentTime { get; set; }

        public Dictionary<string, decimal> SensorReadings { get; set; }

        public DateTime GetCurrentTime()
        {
            return DateTime.Parse(CurrentTime);
        }

        public DateTime Now { get { return GetCurrentTime(); } }

        public decimal GetNumericSensorReading(string sensorName)
        {
            return SensorReadings[sensorName];
        }

        public string GetStringSensorReading(string sensorName)
        {
            throw new NotImplementedException();
        }
    }
    
    [Collection("DatabaseCollection")]
    public class DeviceTriggerServiceTests 
    {

        private Device CreateTestDevice(AtHomeDbContext ctx, string name, string trigger1, string trigger2)
        {
            var stateOn = ctx.DeviceStates.Single(s => s.Name == "On");
            var stateOff = ctx.DeviceStates.Single(s => s.Name == "Off");

            ctx.Devices.Add(new Device(stateOff, stateOn)
            {
                Name = name,
                ImageName = "TestImg",
                Sequence = 0,
                Triggers = new List<DeviceTrigger>
                {
                    new DeviceTrigger {ToState = stateOn, Sequence = 1, TriggerText = trigger1, IsActive = true},
                    new DeviceTrigger {ToState = stateOff, Sequence = 2, TriggerText = trigger2, IsActive = true}

                }
            });

            ctx.SaveChanges();

            return ctx.Devices.Include("Triggers").Single(d => d.Name == name);
        }

        [Fact]
        public void AtTriggerWithNoAfter()
        {
            // Arrange
            using (var ctx = new AtHomeDbContext())
            {
                var device = CreateTestDevice(ctx, "AtTriggerWithNoAfter", "AT 5:00pm", "AT 11:00pm");
                var helpers = new FakeHelpers {CurrentTime = "5:01pm"};
                var triggerComp = new TriggerCompiler(helpers);
                var stateSvc = new Mock<IDeviceStateService>();

                var underTest = new DeviceTriggerService(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                underTest.ProcessTriggers(device);

                // Assert
                stateSvc.Verify(s => s.ChangeDeviceState(device, It.Is<DeviceState>(ds => ds.Name == "On")), Times.Once());
                SqlAssert.Count("SELECT COUNT(*) FROM DeviceTriggerWait WHERE DeviceTriggerId = " + device.Triggers[1].Id, 0);
            }
        }

        [Fact]
        public void AtTriggerWithAfter()
        {
            // Arrange
            using (var ctx = new AtHomeDbContext())
            {
                var device = CreateTestDevice(ctx, "AtTriggerWithAfter", "at 5:00pm", "after 2 hours");
                var helpers = new FakeHelpers { CurrentTime = "5:01pm" };
                var triggerComp = new TriggerCompiler(helpers);
                var stateSvc = new Mock<IDeviceStateService>();

                var underTest = new DeviceTriggerService(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                underTest.ProcessTriggers(device);

                // Assert
                stateSvc.Verify(s => s.ChangeDeviceState(device, It.Is<DeviceState>(ds => ds.Name == "On")), Times.Once());
                SqlAssert.Count("SELECT COUNT(*) FROM DeviceTriggerWait WHERE DeviceTriggerId = " + device.Triggers[1].Id, 1);
            }
        }

        [Fact]
        public void WhenTriggerWithNoAfter()
        {
            // Arrange
            using (var ctx = new AtHomeDbContext())
            {
                var device = CreateTestDevice(ctx, "WhenTriggerWithNoAfter", "WHEN LightLevel  > 0.5", "AT 11:00pm");
                var helpers = new FakeHelpers
                {
                    CurrentTime = "5:01pm",
                    SensorReadings = new Dictionary<string, decimal> {{"lightlevel", 0.6m}}
                };
                var triggerComp = new TriggerCompiler(helpers);
                var stateSvc = new Mock<IDeviceStateService>();

                var underTest = new DeviceTriggerService(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                underTest.ProcessTriggers(device);

                // Assert
                stateSvc.Verify(s => s.ChangeDeviceState(device, It.Is<DeviceState>(ds => ds.Name == "On")), Times.Once());
                SqlAssert.Count("SELECT COUNT(*) FROM DeviceTriggerWait WHERE DeviceTriggerId = " + device.Triggers[1].Id, 0);
            }
            
        }

        [Fact]
        public void WhenTriggerWithAfter()
        {
            // Arrange
            using (var ctx = new AtHomeDbContext())
            {
                var device = CreateTestDevice(ctx, "WhenTriggerWithAfter", "WHEN LightLevel  > 0.5", "After 14 minutes");
                var helpers = new FakeHelpers
                {
                    CurrentTime = "5:01pm",
                    SensorReadings = new Dictionary<string, decimal> { { "lightlevel", 0.6m } }
                };
                var triggerComp = new TriggerCompiler(helpers);
                var stateSvc = new Mock<IDeviceStateService>();

                var underTest = new DeviceTriggerService(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                underTest.ProcessTriggers(device);

                // Assert
                stateSvc.Verify(s => s.ChangeDeviceState(device, It.Is<DeviceState>(ds => ds.Name == "On")), Times.Once());
                SqlAssert.Count("SELECT COUNT(*) FROM DeviceTriggerWait WHERE DeviceTriggerId = " + device.Triggers[1].Id, 1);
            }

        }

        [Fact]
        public void AfterTriggerFromAt()
        {
            // Arrange
            using (var ctx = new AtHomeDbContext())
            {
                var device = CreateTestDevice(ctx, "AfterTriggerFromAt", "at 5:00pm", "after 2 hours");
                var helpers = new FakeHelpers { CurrentTime = "5:01pm" };
                var triggerComp = new TriggerCompiler(helpers);
                var stateSvc = new Mock<IDeviceStateService>();

                var underTest = new DeviceTriggerService(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                underTest.ProcessTriggers(device);

                // Assert
                stateSvc.Verify(s => s.ChangeDeviceState(device, It.Is<DeviceState>(ds => ds.Name == "On")), Times.Once());
                SqlAssert.Count("SELECT COUNT(*) FROM DeviceTriggerWait WHERE DeviceTriggerId = " + device.Triggers[1].Id, 1);

                // Make it look two hours later to ensure the after will fire
                helpers.CurrentTime = "7:01pm";

                // Act
                underTest.ProcessTriggers(device);
                
                // Assert
                stateSvc.Verify(s => s.ChangeDeviceState(device, It.Is<DeviceState>(ds => ds.Name == "Off")), Times.Once());
                SqlAssert.Count("SELECT COUNT(*) FROM DeviceTriggerWait WHERE DeviceTriggerId = " + device.Triggers[1].Id, 0);
            }
        }

        [Fact]
        public void AfterTriggerFromWhen()
        {
            // Arrange
            using (var ctx = new AtHomeDbContext())
            {
                var device = CreateTestDevice(ctx, "AfterTriggerFromWhen", "WHEN LightLevel  > 0.5", "After 14 minutes");
                var helpers = new FakeHelpers
                {
                    CurrentTime = "5:01pm",
                    SensorReadings = new Dictionary<string, decimal> { { "lightlevel", 0.6m } }
                };
                var triggerComp = new TriggerCompiler(helpers);
                var stateSvc = new Mock<IDeviceStateService>();

                var underTest = new DeviceTriggerService(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                underTest.ProcessTriggers(device);

                // Assert
                stateSvc.Verify(s => s.ChangeDeviceState(device, It.Is<DeviceState>(ds => ds.Name == "On")), Times.Once());
                SqlAssert.Count("SELECT COUNT(*) FROM DeviceTriggerWait WHERE DeviceTriggerId = " + device.Triggers[1].Id, 1);

                // Make it look 13 minutes later to make sure after won't fire yet...
                helpers.CurrentTime = "5:14pm";

                // Act
                underTest.ProcessTriggers(device);

                // Assert
                stateSvc.Verify(s => s.ChangeDeviceState(device, It.Is<DeviceState>(ds => ds.Name == "Off")), Times.Never);
                SqlAssert.Count("SELECT COUNT(*) FROM DeviceTriggerWait WHERE DeviceTriggerId = " + device.Triggers[1].Id, 1);

                // Now make it 14 minutes after start to ensure trigger fires
                helpers.CurrentTime = "5:15pm";

                // Act
                underTest.ProcessTriggers(device);

                // Assert
                stateSvc.Verify(s => s.ChangeDeviceState(device, It.Is<DeviceState>(ds => ds.Name == "Off")), Times.Once);
                SqlAssert.Count("SELECT COUNT(*) FROM DeviceTriggerWait WHERE DeviceTriggerId = " + device.Triggers[1].Id, 0);
            }

        }
    }
}
