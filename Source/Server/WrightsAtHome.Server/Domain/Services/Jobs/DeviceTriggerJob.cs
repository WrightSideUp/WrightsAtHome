using System;
using System.Data.Entity;
using System.Linq;
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
        private readonly IAtHomeDbContext dbContext;
        private readonly IDeviceService deviceService;
        private readonly ILogger logger;

        public DeviceTriggerJob(IAtHomeDbContext dbContext, IDeviceService deviceService)
        {
            this.dbContext = dbContext;
            this.deviceService = deviceService;

            logger = LogManager.GetCurrentClassLogger();
        }

        public void MonitorSchedules()
        {
            try
            {
                logger.Info("Device Trigger Processing Job Begun");

                var devicesWithTriggersIds = dbContext.Devices.Where(d => d.Triggers.Any(t => t.IsActive)).Select(d => d.Id).ToList();

                foreach (var deviceId in devicesWithTriggersIds)
                {
                    deviceService.ProcessTriggers(deviceId);
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