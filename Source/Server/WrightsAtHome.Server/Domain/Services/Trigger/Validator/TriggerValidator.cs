using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser;

namespace WrightsAtHome.Server.Domain.Services.Trigger.Validator
{
    public interface ITriggerValidator
    {
        TriggerValidationInfo ValidateTrigger(string triggerText);
    }

    public class TriggerValidator : ITriggerValidator
    {
        private readonly ITriggerHelpers helpers;

        public TriggerValidator(ITriggerHelpers helpers)
        {
            this.helpers = helpers;
        }
        
        public TriggerValidationInfo ValidateTrigger(string triggerText)
        {
            var errorListener = new TriggerValidatorErrors(triggerText);
            var listener = new TriggerValidatorListener(helpers, errorListener);
            var parseHelper = new TriggerParseHelper(listener, errorListener);

            parseHelper.ParseTrigger(triggerText);

            var result = new TriggerValidationInfo
            {
                IsValid = (errorListener.Errors.Count == 0),
                TriggerType = listener.TriggerType,
            };

            result.Errors = errorListener.Errors;

            return result;
        }
    }
}