
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.API.Devices.Model
{
    public class TriggerValidationErrorDto
    {
        public string BadToken { get; set; }

        public int StartIndex { get; set; }

        public int Length { get; set; }

        public string ErrorMessage { get; set; }
    }
    
    public class TriggerValidationDto
    {
        public bool IsValid { get; set; }

        public string TriggerType { get; set; }

        public TriggerValidationErrorDto[] Errors { get; set; }
    }
}