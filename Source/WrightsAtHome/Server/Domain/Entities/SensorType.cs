
using System.Collections.Generic;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class SensorType
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public List<Sensor> Sensors { get; set; }
    }
}
