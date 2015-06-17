using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NLog;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.Domain.Services.Devices.Internal
{
    public interface IDeviceTriggerProcessor
    {
        // Evaluate all triggers associated with a device and change the 
        // device state where appropriate.
        void ProcessTriggers(int deviceId);
    }

    public class DeviceTriggerProcessor : IDeviceTriggerProcessor
    {
        private readonly IAtHomeDbContext dbContext;
        private readonly IDeviceTriggerCompiler triggerCompiler;
        private readonly IDeviceStateService deviceStateService;
        private readonly IDateTimeHelpers dateTimeHelpers;
        private readonly Logger logger;

        public DeviceTriggerProcessor(
            IAtHomeDbContext dbContext, 
            IDeviceTriggerCompiler triggerCompiler, 
            IDeviceStateService deviceStateService, 
            IDateTimeHelpers dateTimeHelpers)
        {
            this.dbContext = dbContext;
            this.triggerCompiler = triggerCompiler;
            this.deviceStateService = deviceStateService;
            this.dateTimeHelpers = dateTimeHelpers;
            logger = LogManager.GetCurrentClassLogger();
        }

        public void ProcessTriggers(int deviceId)
        {
            try
            {
                var device = FetchDeviceWithTriggers(deviceId);

                logger.Info("Processing Triggers for Device {0}", device.Name);

                IList<TriggerContext> compiledTriggers = triggerCompiler.CompileTriggers(device);

                // Have to use for-loop because we need to 'look ahead' to see if next trigger is an After trigger
                foreach (var context in compiledTriggers)
                {
                    if (context.Primary.TriggerType != TriggerType.After)
                    {
                        ProcessAtOrWhenTrigger(device, context);
                    }
                    else
                    {
                        ProcessAfterTrigger(device, context);
                    }
                }

                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unexpected Error in Trigger Processing");
                throw;
            }
        }

        private Device FetchDeviceWithTriggers(int deviceId)
        {
            var device = dbContext.Devices.SingleOrDefault(d => d.Id == deviceId);

            if (device == null)
            {
                var msg = string.Format("No Device with Id {0} exists", deviceId);
                var ex = new ArgumentException(msg, "deviceId");
                logger.Error(msg, ex);
                throw ex;
            }

            dbContext.Entry(device)
                .Collection(d => d.Triggers)
                .Query()
                .Include(t => t.ToState)
                .Where(t => t.IsActive)
                .OrderBy(t => t.Sequence)
                .Load();

            return device;
        }

        private void ProcessAtOrWhenTrigger(Device device, TriggerContext context)
        {
            // Call the trigger function to see if it's condition is true
            if (context.Primary.AtOrWhenFunction())
            {
                ChangeDeviceState(device, context.Primary.Trigger);

                // See if the next trigger is an After trigger and needs a wait state
                if (context.Dependent != null)
                {
                    bool alreadyWaiting = dbContext.DeviceTriggerWaits.Any(w => w.AfterTrigger.Id == context.Dependent.Trigger.Id);

                    if (!alreadyWaiting)
                    {
                        // Create new wait state so After trigger knows how long to wait
                        dbContext.DeviceTriggerWaits.Add(new DeviceTriggerWait
                        {
                            AfterTrigger = context.Dependent.Trigger,
                            StartTime = dateTimeHelpers.Now
                        });
                    }
                }
            }
        }

        private void ChangeDeviceState(Device device, DeviceTrigger trigger)
        {
            logger.Info("Attempting state change on device {0} to {1} due to trigger ID {2}", device.Name,
                trigger.ToState, trigger.Id);

            deviceStateService.ChangeDeviceState(device, trigger.ToState);
        }

        private void ProcessAfterTrigger(Device device, TriggerContext context)
        {
            // See if there's a wait state for this trigger
            DeviceTriggerWait wait = dbContext.DeviceTriggerWaits
                .FirstOrDefault(w => w.AfterTrigger.Id == context.Primary.Trigger.Id);

            if (wait != null)
            {
                if (context.Primary.AfterFunction(wait.StartTime))
                {
                    ChangeDeviceState(device, context.Primary.Trigger);

                    // Delete the wait state
                    dbContext.DeviceTriggerWaits.Remove(wait);
                }
            }
        }
    }
}