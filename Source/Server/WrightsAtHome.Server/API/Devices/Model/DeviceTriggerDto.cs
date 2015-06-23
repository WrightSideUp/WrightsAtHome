using System.ComponentModel.DataAnnotations;

namespace WrightsAtHome.Server.API.Devices.Model
{
    public class DeviceTriggerDto
    {
        public int Id { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required]
        public int ToStateId { get; set; }

        [Required]
        [MaxLength(1024)]
        public string TriggerText { get; set; }

        [Required]
        public int Sequence { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}