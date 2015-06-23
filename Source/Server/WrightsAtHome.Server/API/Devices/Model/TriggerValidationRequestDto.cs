using System.ComponentModel.DataAnnotations;

namespace WrightsAtHome.Server.API.Devices.Model
{
    public class TriggerValidationRequestDto
    {
        [Required]
        [MaxLength(1024)]
        public string TriggerText { get; set; }
    }
}