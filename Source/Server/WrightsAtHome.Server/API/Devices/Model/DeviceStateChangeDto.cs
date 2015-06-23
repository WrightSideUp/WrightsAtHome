using System.ComponentModel.DataAnnotations;

namespace WrightsAtHome.Server.API.Devices.Model
{
    public class DeviceStateChangeDto
    {
        [Required]
        public int StateId { get; set; }
    }
}
