using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser;

namespace WrightsAtHome.Server.Domain.Services.Trigger.Compiler
{
    public interface ITriggerCompiler
    {
        TriggerInfo CompileTrigger(DeviceTrigger trigger);
    }

    public class TriggerCompiler : ITriggerCompiler
    {
        private readonly ITriggerHelpers helpers;

        public TriggerCompiler(ITriggerHelpers helpers)
        {
            this.helpers = helpers;
        }

        public TriggerInfo CompileTrigger(DeviceTrigger trigger)
        {
            var listener = new TriggerCompilerListener(helpers);
            var errorListener = new TriggerCompilerErrors();
            var parseHelper = new TriggerParseHelper(listener, errorListener);

            parseHelper.ParseTrigger(trigger.TriggerText);

            var result = new TriggerInfo
            {
                Trigger = trigger,
                TriggerType = listener.TriggerType,
                TriggerText = trigger.TriggerText,
                TriggerStartTime = listener.AtTime,
                TriggerAfterDelay = listener.AfterDelay,
                EventDescription = GetEventDescription(trigger, listener.TriggerType, listener.ReferencedSensors)
            };

            if (result.TriggerType == TriggerType.After)
            {
                var lambda = Expression.Lambda<Func<DateTime, bool>>(listener.Result, listener.Parameters);
                result.AfterFunction = lambda.Compile();
            }
            else
            {
                var lambda = Expression.Lambda<Func<bool>>(listener.Result);
                result.AtOrWhenFunction = lambda.Compile();
            }

            return result;
        }

        private string GetEventDescription(DeviceTrigger trigger, TriggerType triggerType, Dictionary<string, string> sensors)
        {
            var result = trigger.ToState != null
                ? "Turn " + trigger.ToState.Name + " " + trigger.TriggerText.ToLower()
                : trigger.TriggerText.ToLower();

            if (triggerType == TriggerType.After)
            {
                result = result.Replace("after ", "AFTER ");
            }
            else if (triggerType == TriggerType.At)
            {
                result = result.Replace("at ", "AT ");
            }
            else if (triggerType == TriggerType.When)
            {
                result = result.Replace("when ", "WHEN ");
            }

            foreach (var pair in sensors)
            {
                result = result.Replace(pair.Key, pair.Value);
            }
            
            return result;
        }
    }
}
