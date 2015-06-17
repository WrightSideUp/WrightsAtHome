using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.Tests.Integration.Utility
{
    public static class DeviceUtils
    {
        public static Device CreateTestDevice(AtHomeDbContext ctx, string name, string trigger1, string trigger2)
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
    }
}
