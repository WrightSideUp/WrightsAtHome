using System;

namespace WrightsAtHome.BackEnd.Domain.Entities
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SmallImageUrl { get; set; }
        public string LargeImageUrl { get; set; }
        public SensorType SensorType { get; set; }
        public SensorReading LastReading { get; set; }
        public TimeSpan ReadInterval { get; set; }
    }
}
