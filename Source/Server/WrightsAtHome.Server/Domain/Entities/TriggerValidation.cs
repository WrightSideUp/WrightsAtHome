using System.Collections.Generic;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class DeviceTriggerValidationInfo
    {
        public bool IsValid { get; set; }

        public string ErrorMessage { get; set; }

        public List<TriggerValidationInfo> TriggerValidations { get; set; }

        public DeviceTriggerValidationInfo()
        {
            TriggerValidations = new List<TriggerValidationInfo>();
        }
    }
    
    public class TriggerValidationInfo
    {
        public bool IsValid { get; set; }

        public TriggerType TriggerType { get; set; }

        public List<TriggerValidationError> Errors { get; set; }

        public TriggerValidationInfo()
        {
            Errors = new List<TriggerValidationError>();
        }

    }

    public class TriggerValidationError
    {
        public string BadToken { get; set; }

        public int StartIndex { get; set; }

        public int Length { get; set; }

        public string ErrorMessage { get; set; }
    }
}