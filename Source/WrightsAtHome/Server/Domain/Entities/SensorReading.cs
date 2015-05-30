using System;

namespace WrightsAtHome.BackEnd.Domain.Entities
{
    public class SensorReading
    {
        public int Id { get; set; }
        public DateTime ReadingDate { get; set; }
        public decimal Value { get; set; }
    }
}
