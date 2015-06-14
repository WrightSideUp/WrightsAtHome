using System;
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

    public class DeviceTriggerService : IDeviceTriggerService
    {
        private readonly IAtHomeDbContext dbContext;
        private readonly ITriggerCompiler triggerCompiler;
        private readonly IDeviceStateService deviceStateService;

        private DeviceTriggerService(IAtHomeDbContext dbContext, ITriggerCompiler triggerCompiler, IDeviceStateService deviceStateService)
        {
            this.dbContext = dbContext;
            this.triggerCompiler = triggerCompiler;
            this.deviceStateService = deviceStateService;
        }

        public string GetNextTriggerEvent(int deviceId)
        {
            return string.Empty;
        }

        public void ProcessTriggers(Device device)
        {
            var triggersToEvaluate = device.Triggers.OrderBy(t => t.Sequence).ToList();

            // Have to use for-loop because we need to 'look ahead' to see if next trigger is an After trigger
            for (int triggerNdx = 0; triggerNdx < triggersToEvaluate.Count; triggerNdx++)
            {
                if (!triggerCompiler.IsAfterTrigger(triggersToEvaluate[triggerNdx].TriggerText))
                {
                    ProcessAtOrWhenTrigger(device, triggersToEvaluate, triggerNdx);
                }
                else
                {
                    ProcessAfterTrigger(device, triggersToEvaluate[triggerNdx]);
                }
            }

            dbContext.SaveChanges();
        }

        private void ProcessAtOrWhenTrigger(Device device, List<DeviceTrigger> triggersToEvaluate, int triggerNdx)
        {
            DeviceTrigger trigger = triggersToEvaluate[triggerNdx];

            // Call the trigger function to see if it's condition is true
            Func<bool> triggerFunc = triggerCompiler.CompileAtOrWhenTrigger(trigger.TriggerText);

            if (triggerFunc())
            {
                deviceStateService.ChangeDeviceState(device, trigger.ToState);

                // See if the next trigger is an After trigger and needs a wait state
                if (triggerNdx < triggersToEvaluate.Count - 1)
                {
                    var nextTrigger = triggersToEvaluate[triggerNdx + 1];

                    if (triggerCompiler.IsAfterTrigger(nextTrigger.TriggerText))
                    {
                        bool alreadyWaiting = dbContext.DeviceTriggerWaits.Any(w => w.AfterTrigger.Id == nextTrigger.Id);

                        if (!alreadyWaiting)
                        {
                            // Create new wait state so After trigger knows how long to wait
                            dbContext.DeviceTriggerWaits.Add(new DeviceTriggerWait
                            {
                                AfterTrigger = nextTrigger,
                                StartTime = DateTime.Now
                            });
                        }
                    }
                }
            }
        }

        private void ProcessAfterTrigger(Device device, DeviceTrigger trigger)
        {
            // See if there's a wait state for this trigger
            DeviceTriggerWait wait = dbContext.DeviceTriggerWaits
                .FirstOrDefault(w => w.AfterTrigger.Id == trigger.Id);

            if (wait != null)
            {
                Func<DateTime, bool> triggerFunc = triggerCompiler.CompileAfterTrigger(trigger.TriggerText);

                if (triggerFunc(wait.StartTime))
                {
                    deviceStateService.ChangeDeviceState(device, trigger.ToState);

                    // Delete the wait state
                    dbContext.DeviceTriggerWaits.Remove(wait);
                }
            }
        }
    }
}