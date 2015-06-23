using System;
using System.Linq;
using Autofac;
using NLog;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Services.Devices;

namespace WrightsAtHome.Server.Domain.Services.Jobs
{
    public interface IDeviceTriggerJob
    {
        void MonitorSchedules();
    }

    public class DeviceTriggerJob : IDeviceTriggerJob
    {
        private readonly ILifetimeScope parentScope;
        private readonly ILogger logger;

        public DeviceTriggerJob(ILifetimeScope parentScope)
        {
            this.parentScope = parentScope;
            logger = LogManager.GetCurrentClassLogger();
        }

        public void MonitorSchedules()
        {
            try
            {
                logger.Info("Device Trigger Processing Job Begun");

                using (var autoFacScope = parentScope.BeginLifetimeScope())
                {
                    var dbContext = autoFacScope.Resolve<IAtHomeDbContext>();
                    var deviceService = autoFacScope.Resolve<IDeviceService>();

                    var devicesWithTriggersIds =
                        dbContext.Devices.Where(d => d.Triggers.Any(t => t.IsActive)).Select(d => d.Id).ToList();

                    foreach (var deviceId in devicesWithTriggersIds)
                    {
                        deviceService.ProcessTriggers(deviceId);
                    }
                }

                logger.Info("Device Trigger Processing Job Ended");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Fatal error processing Device Triggers");
            }
        }
    }
}