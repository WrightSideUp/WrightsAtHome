using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class SensorReading
    {
        public int Id { get; set; }

        public Sensor ReadingSensor { get; set; }

        public DateTime ReadingDate { get; set; }

        public decimal Value { get; set; }
    }
}
