using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.Domain.Services.Devices.Internal
{
    public interface IDeviceTriggerEventService
    {
        // Determine the next trigger event that is likely to fire and return 
        // its description.
        string GetNextTriggerEvent(int deviceId);
    }

    public class DeviceTriggerEventService : IDeviceTriggerEventService
    {
        private readonly IAtHomeDbContext dbContext;
        private readonly IDeviceTriggerCompiler triggerCompiler;
        private readonly IDeviceStateService deviceStateService;
        private readonly IDateTimeHelpers dateTimeHelpers;
        private readonly Logger logger;


        public DeviceTriggerEventService(
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

        public string GetNextTriggerEvent(int deviceId)
        {
            var device = FetchDeviceWithTriggers(deviceId);

            if (!device.Triggers.Any())
            {
                return "None";
            }

            IList<TriggerContext> compiledTriggers = triggerCompiler.CompileTriggers(device);

            // Only triggers whose state is different than the current state can be next
            DeviceState currentState = deviceStateService.GetCurrentDeviceState(deviceId);
            var possibleTriggers = compiledTriggers.Where(ct => ct.Primary.Trigger.ToState.Id != currentState.Id).ToList();

            return CheckAfterTriggers(possibleTriggers) ??
                   CheckWhenTriggers(possibleTriggers) ??
                   CheckAtTriggers(possibleTriggers) ??
                   "None";
        }

        private string CheckAfterTriggers(List<TriggerContext> possibleTriggers)
        {
            // If there is an After with an active wait state then that's the next event.
            // After triggers that aren't waiting can never be the next event since
            // the initial event has to happen first before the after starts waiting.

            var afterTriggers =
                possibleTriggers.Where(p => p.Primary.TriggerType == TriggerType.After)
                    .Select(p => p.Primary)
                    .ToDictionary(p => p.Trigger.Id, p => p);

            var waitingTriggers = dbContext.DeviceTriggerWaits
                .Where(w => afterTriggers.Keys.Contains(w.AfterTrigger.Id)).ToList();

            // If there are multiple After triggers waiting, get the one that ends soonest
            var firstEnding = waitingTriggers
            .OrderBy(w => w.StartTime + afterTriggers[w.AfterTrigger.Id].TriggerAfterDelay)
            .FirstOrDefault();

            if (firstEnding != null)
            {
                var info = afterTriggers[firstEnding.AfterTrigger.Id];

                string endTime = string.Format(" [will happen at {0:t}]", firstEnding.StartTime + info.TriggerAfterDelay);

                return info.EventDescription + endTime;
            }

            return null;
        }

        private string CheckWhenTriggers(List<TriggerContext> possibleTriggers)
        {
            // If there is a when trigger, then it is next
            var whenTrigger =
                possibleTriggers.Where(p => p.Primary.TriggerType == TriggerType.When)
                    .OrderBy(p => p.Primary.Trigger.Sequence)
                    .FirstOrDefault();

            if (whenTrigger != null)
            {
                return whenTrigger.Primary.EventDescription;
            }

            return null;
        }

        private string CheckAtTriggers(List<TriggerContext> possibleTriggers)
        {
            // Find the at trigger with earliest start time > now and that will be next
            var atTriggers =
                possibleTriggers.Where(p => p.Primary.TriggerType == TriggerType.At)
                    .OrderBy(p => p.Primary.TriggerStartTime)
                    .Select(p => p.Primary).ToList();

            var firstGreaterThanNow =
                atTriggers.FirstOrDefault(
                p => dateTimeHelpers.Now.Date + p.TriggerStartTime.TimeOfDay > dateTimeHelpers.Now);

            if (firstGreaterThanNow != null)
            {
                return firstGreaterThanNow.EventDescription;
            }

            // There wasn't one greater than now, so return the first At trigger we find (it will fire tomorrow)
            var firstTrigger = atTriggers.OrderBy(a => a.TriggerStartTime).FirstOrDefault();

            return firstTrigger != null ? firstTrigger.EventDescription + " tomorrow" : null;
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
    }
}