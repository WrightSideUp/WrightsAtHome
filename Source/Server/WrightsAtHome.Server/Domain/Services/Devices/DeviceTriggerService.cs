using System.Collections.Generic;
using System.Linq;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Trigger;

namespace WrightsAtHome.Server.Domain.Services.Devices
{
    public interface IDeviceTriggerService
    {
        string GetNextTriggerEvent(int deviceId);

        void ProcessTriggers(Device device);
    }

    class TriggerContext
    {
        public TriggerInfo Primary { get; set; }

        public TriggerInfo Dependent { get; set; }
    }

    public class DeviceTriggerService : IDeviceTriggerService
    {
        private readonly IAtHomeDbContext dbContext;
        private readonly ITriggerCompiler triggerCompiler;
        private readonly IDeviceStateService deviceStateService;
        private readonly IDateTimeHelpers dateTimeHelpers;

        public DeviceTriggerService(
            IAtHomeDbContext dbContext, 
            ITriggerCompiler triggerCompiler, 
            IDeviceStateService deviceStateService, 
            IDateTimeHelpers dateTimeHelpers)
        {
            this.dbContext = dbContext;
            this.triggerCompiler = triggerCompiler;
            this.deviceStateService = deviceStateService;
            this.dateTimeHelpers = dateTimeHelpers;
        }

        public string GetNextTriggerEvent(int deviceId)
        {
            return string.Empty;
        }

        public void ProcessTriggers(Device device)
        {
            IList<TriggerContext> compiledTriggers = CompileTriggers(device);

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

        private IList<TriggerContext> CompileTriggers(Device device)
        {
            // Create list of all compiled triggers
            var result = 
                (from trigger in device.Triggers
                where trigger.IsActive
                orderby trigger.Sequence
                select new TriggerContext { Primary = triggerCompiler.CompileTrigger(trigger) }).ToList();

            

            // Loop through list and create context for each trigger (assigning dependent triggers for After triggers)
            for (int i = 0; i < result.Count; i++)
            {
                if (i < result.Count - 1)
                    if (result[i + 1].Primary.TriggerType == TriggerType.After)
                    {
                        result[i].Dependent = result[i + 1].Primary;
                    }
            }

            return result;
        }
        
        private void ProcessAtOrWhenTrigger(Device device, TriggerContext context)
        {
            // Call the trigger function to see if it's condition is true
            if (context.Primary.AtOrWhenFunction())
            {
                deviceStateService.ChangeDeviceState(device, context.Primary.Trigger.ToState);

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

        private void ProcessAfterTrigger(Device device, TriggerContext context)
        {
            // See if there's a wait state for this trigger
            DeviceTriggerWait wait = dbContext.DeviceTriggerWaits
                .FirstOrDefault(w => w.AfterTrigger.Id == context.Primary.Trigger.Id);

            if (wait != null)
            {
                if (context.Primary.AfterFunction(wait.StartTime))
                {
                    deviceStateService.ChangeDeviceState(device, context.Primary.Trigger.ToState);

                    // Delete the wait state
                    dbContext.DeviceTriggerWaits.Remove(wait);
                }
            }
        }
    }
}