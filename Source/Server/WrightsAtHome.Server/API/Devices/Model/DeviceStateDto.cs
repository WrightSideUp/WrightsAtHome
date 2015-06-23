using System.ComponentModel.DataAnnotations;

namespace WrightsAtHome.Server.API.Devices.Model
{
    public class DeviceStateDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        [Required]
        public int Sequence { get; set; }
    }
}