
using System.ComponentModel.DataAnnotations;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.API.Devices.Model
{
    public class DeviceDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public int Sequence { get; set; }

        public DeviceStateDto[] PossibleStates { get; set; }

        public int CurrentStateId { get; set; }
        
        public string SmallImageUrl { get; set; }

        public string LargeImageUrl { get; set; }

        public string NextEvent { get; set; }
    }
}
