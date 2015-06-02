
using System.Collections.Generic;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class DeviceState
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StateNumber { get; set; }

        public List<Device> Devices { get; set; } 
    }
}
