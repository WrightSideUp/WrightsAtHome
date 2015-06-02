using System;
using System.Collections.Generic;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class Sensor
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string ImageName { get; set; }
        
        public SensorType SensorType { get; set; }
        
        public TimeSpan ReadInterval { get; set; }

        public List<SensorReading> Readings { get; set; }
    }
}
