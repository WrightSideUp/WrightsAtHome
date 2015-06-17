using System;
using System.Collections.Generic;
using Moq;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Devices.Internal;
using WrightsAtHome.Server.Domain.Services.Trigger;
using WrightsAtHome.Server.Tests.Integration.Utility;
using WrightsAtHome.Server.Tests.Unit.Utility;
using WrightsAtHome.Tests.Integration.Utility;
using Xunit;

namespace WrightsAtHome.Server.Tests.Integration.Domain.Services.Devices
{
    [Collection("DatabaseCollection")]
    public class DeviceTriggerProcessorTests 
    {
        [Fact]
        public void AtTriggerWithNoAfter()
        {
            // Arrange
            using (var ctx = new AtHomeDbContext())
            {
                var device = DeviceUtils.CreateTestDevice(ctx, "AtTriggerWithNoAfter", "AT 5:00pm", "AT 11:00pm");
                var helpers = new TriggerHelpersFake {CurrentTime = "5:01pm"};
                var triggerComp = new DeviceTriggerCompiler(new TriggerCompiler(helpers));
                var stateSvc = new Mock<IDeviceStateService>();

                ctx.SaveChanges();

                var underTest = new DeviceTriggerProcessor(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                underTest.ProcessTriggers(device.Id);

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
                var device = DeviceUtils.CreateTestDevice(ctx, "AtTriggerWithAfter", "at 5:00pm", "after 2 hours");
                var helpers = new TriggerHelpersFake { CurrentTime = "5:01pm" };
                var triggerComp = new DeviceTriggerCompiler(new TriggerCompiler(helpers));
                var stateSvc = new Mock<IDeviceStateService>();
                ctx.SaveChanges();
                
                
                var underTest = new DeviceTriggerProcessor(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                underTest.ProcessTriggers(device.Id);

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
                var device = DeviceUtils.CreateTestDevice(ctx, "WhenTriggerWithNoAfter", "WHEN LightLevel  > 0.5", "AT 11:00pm");
                var helpers = new TriggerHelpersFake
                {
                    CurrentTime = "5:01pm",
                    SensorReadings = new Dictionary<string, decimal> {{"lightlevel", 0.6m}}
                };
                var triggerComp = new DeviceTriggerCompiler(new TriggerCompiler(helpers));
                var stateSvc = new Mock<IDeviceStateService>();

                ctx.SaveChanges();

                var underTest = new DeviceTriggerProcessor(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                underTest.ProcessTriggers(device.Id);

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
                var device = DeviceUtils.CreateTestDevice(ctx, "WhenTriggerWithAfter", "WHEN LightLevel  > 0.5", "After 14 minutes");
                var helpers = new TriggerHelpersFake
                {
                    CurrentTime = "5:01pm",
                    SensorReadings = new Dictionary<string, decimal> { { "lightlevel", 0.6m } }
                };
                var triggerComp = new DeviceTriggerCompiler(new TriggerCompiler(helpers));
                var stateSvc = new Mock<IDeviceStateService>();

                ctx.SaveChanges();

                var underTest = new DeviceTriggerProcessor(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                underTest.ProcessTriggers(device.Id);

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
                var device = DeviceUtils.CreateTestDevice(ctx, "AfterTriggerFromAt", "at 5:00pm", "after 2 hours");
                var helpers = new TriggerHelpersFake { CurrentTime = "5:01pm" };
                var triggerComp = new DeviceTriggerCompiler(new TriggerCompiler(helpers));
                var stateSvc = new Mock<IDeviceStateService>();

                ctx.SaveChanges();

                var underTest = new DeviceTriggerProcessor(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                underTest.ProcessTriggers(device.Id);

                // Assert
                stateSvc.Verify(s => s.ChangeDeviceState(device, It.Is<DeviceState>(ds => ds.Name == "On")), Times.Once());
                SqlAssert.Count("SELECT COUNT(*) FROM DeviceTriggerWait WHERE DeviceTriggerId = " + device.Triggers[1].Id, 1);

                // Make it look two hours later to ensure the after will fire
                helpers.CurrentTime = "7:01pm";

                // Act
                underTest.ProcessTriggers(device.Id);
                
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
                var device = DeviceUtils.CreateTestDevice(ctx, "AfterTriggerFromWhen", "WHEN LightLevel  > 0.5", "After 14 minutes");
                var helpers = new TriggerHelpersFake
                {
                    CurrentTime = "5:01pm",
                    SensorReadings = new Dictionary<string, decimal> { { "lightlevel", 0.6m } }
                };
                var triggerComp = new DeviceTriggerCompiler(new TriggerCompiler(helpers));
                var stateSvc = new Mock<IDeviceStateService>();

                ctx.SaveChanges();

                var underTest = new DeviceTriggerProcessor(ctx, triggerComp, stateSvc.Object, helpers);
                
                // Act
                underTest.ProcessTriggers(device.Id);

                // Assert
                stateSvc.Verify(s => s.ChangeDeviceState(device, It.Is<DeviceState>(ds => ds.Name == "On")), Times.Once());
                SqlAssert.Count("SELECT COUNT(*) FROM DeviceTriggerWait WHERE DeviceTriggerId = " + device.Triggers[1].Id, 1);

                // Make it look 13 minutes later to make sure after won't fire yet...
                helpers.CurrentTime = "5:14pm";

                // Act
                underTest.ProcessTriggers(device.Id);

                // Assert
                stateSvc.Verify(s => s.ChangeDeviceState(device, It.Is<DeviceState>(ds => ds.Name == "Off")), Times.Never);
                SqlAssert.Count("SELECT COUNT(*) FROM DeviceTriggerWait WHERE DeviceTriggerId = " + device.Triggers[1].Id, 1);

                // Now make it 14 minutes after start to ensure trigger fires
                helpers.CurrentTime = "5:15pm";

                // Act
                underTest.ProcessTriggers(device.Id);

                // Assert
                stateSvc.Verify(s => s.ChangeDeviceState(device, It.Is<DeviceState>(ds => ds.Name == "Off")), Times.Once);
                SqlAssert.Count("SELECT COUNT(*) FROM DeviceTriggerWait WHERE DeviceTriggerId = " + device.Triggers[1].Id, 0);
            }

        }

        [Fact]
        public void BadDevice()
        {
            using (var ctx = new AtHomeDbContext())
            {
                var helpers = new TriggerHelpersFake
                {
                    CurrentTime = "5:01pm",
                    SensorReadings = new Dictionary<string, decimal> { { "lightlevel", 0.6m } }
                };
                var triggerComp = new DeviceTriggerCompiler(new TriggerCompiler(helpers));
                var stateSvc = new Mock<IDeviceStateService>();

                var underTest = new DeviceTriggerProcessor(ctx, triggerComp, stateSvc.Object, helpers);

                // Act
                var ex = Assert.Throws<ArgumentException>(() => underTest.ProcessTriggers(42));

                // Assert
                Assert.Equal("No Device with Id 42 exists\r\nParameter name: deviceId", ex.Message);
                Assert.Equal("deviceId", ex.ParamName);
            }
        }
    }
}
