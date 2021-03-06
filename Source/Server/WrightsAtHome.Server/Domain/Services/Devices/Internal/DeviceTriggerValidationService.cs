﻿using System.Collections.Generic;
using System.Linq;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Trigger.Compiler;
using WrightsAtHome.Server.Domain.Services.Trigger.Validator;

namespace WrightsAtHome.Server.Domain.Services.Devices.Internal
{
    public interface IDeviceTriggerValidationService
    {
        DeviceTriggerValidationInfo ValidateTriggers(Device device);
    }

    public class DeviceTriggerValidationService : IDeviceTriggerValidationService
    {
        private readonly ITriggerValidator triggerValidator;
        private readonly ITriggerCompiler triggerCompiler;

        public DeviceTriggerValidationService(ITriggerValidator triggerValidator, ITriggerCompiler triggerCompiler)
        {
            this.triggerValidator = triggerValidator;
            this.triggerCompiler = triggerCompiler;
        }

        public DeviceTriggerValidationInfo ValidateTriggers(Device device)
        {
            var result = new DeviceTriggerValidationInfo();
            
            // First perform individual validation on each trigger
            foreach (var trigger in device.Triggers.Where(t => t.IsActive))
            {
                result.TriggerValidations.Add(triggerValidator.ValidateTrigger(trigger.TriggerText));
            }

            var errorTrigger = result.TriggerValidations.FirstOrDefault(tv => !tv.IsValid);

            if (errorTrigger == null)
            {
                if (ValidateToStates(device, result))
                {
                    if (ValidateSequences(device, result))
                    { 
                        // Compile all the triggers so we can validate their relationships
                        var compiledTriggers = device.Triggers.Where(t => t.IsActive).OrderBy(t => t.Sequence).Select(t => triggerCompiler.CompileTrigger(t)).ToList();
                
                        // Check for inter-trigger errors
                        if (ValidateAfterTriggers(compiledTriggers, result))
                        {

                            result.IsValid = true;
                        }
                    }
                }
            }
            else
            {
                result.ErrorMessage = "Invalid Trigger Text";
                result.IsValid = false;
            }

            return result;
        }

        private bool ValidateToStates(Device device, DeviceTriggerValidationInfo result)
        {
            var errorStates = device.Triggers.Where(t => t.IsActive && device.PossibleStates.All(ps => ps.Id != t.ToState.Id)).ToList();

            if (errorStates.Count > 0)
            {
                result.IsValid = false;
                result.ErrorMessage =
                    string.Format(
                        "Invalid ToState '{0}' in Trigger '{1}'.  Trigger state must be one of the possible states for the device.",
                        errorStates[0].ToState.Name, errorStates[0].TriggerText);

                return false;
            }

            return true;
        }

        private bool ValidateSequences(Device device, DeviceTriggerValidationInfo result)
        {
            var dupeSequences =
                device.Triggers.Where(t => t.IsActive)
                    .GroupBy(t => t.Sequence)
                    .Where(g => g.Count() > 1)
                    .Select(g => new { Sequence = g.Key, Triggers = g.ToList() })
                    .ToList();

            if (dupeSequences.Any())
            {
                var firstDupe = dupeSequences.First();
                result.IsValid = false;
                result.ErrorMessage =
                    string.Format("Sequence {0} is repeated in triggers '{1}' and '{2}'.  Sequence must be unique.",
                        firstDupe.Sequence, firstDupe.Triggers[0].TriggerText, firstDupe.Triggers[1].TriggerText);
                return false;
            }

            return true;
        }

        private bool ValidateAfterTriggers(IList<TriggerInfo> compiledTriggers, DeviceTriggerValidationInfo result)
        {
            if (compiledTriggers.Count > 1 && compiledTriggers[0].TriggerType == TriggerType.After)
            {
                result.IsValid = false;
                result.ErrorMessage =
                    "AFTER trigger must not be the first trigger.  AFTER triggers must have a preceding trigger.";

                return false;
            }
            
            return true;
        }
    }
}