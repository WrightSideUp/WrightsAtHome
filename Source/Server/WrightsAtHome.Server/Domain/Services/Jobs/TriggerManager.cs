using System;
using System.Data.Entity;
using System.Linq;
using NLog;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Services.Devices;

namespace WrightsAtHome.Server.Domain.Services.Jobs
{
    public interface ITriggerManager
    {
        void MonitorSchedules();
    }

    public class TriggerManager : ITriggerManager
    {
        private readonly IAtHomeDbContext dbContext;
        private readonly IDeviceTriggerService deviceTriggerService;
        private readonly ILogger logger;

        public TriggerManager(IAtHomeDbContext dbContext, IDeviceTriggerService deviceTriggerService)
        {
            this.dbContext = dbContext;
            this.deviceTriggerService = deviceTriggerService;

            logger = LogManager.GetCurrentClassLogger();
        }

        public void MonitorSchedules()
        {
            try
            {
                logger.Info("Trigger Processing Begun");

                var devicesWithTriggers = dbContext.Devices.Where(d => d.Triggers.Any()).Include(d => d.Triggers).ToList();

                foreach (var device in devicesWithTriggers)
                {
                    deviceTriggerService.ProcessTriggers(device);
                }

                logger.Info("Trigger Processing Ended");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Fatal error reading sensors");
            }
        }
    }
}