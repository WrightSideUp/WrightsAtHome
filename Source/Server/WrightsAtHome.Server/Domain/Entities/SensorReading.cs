using System;
using System.ComponentModel.DataAnnotations;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class SensorReading : IBaseEntity
    {
        public int Id { get; set; }

        public Sensor Sensor { get; set; }

        public DateTime ReadingDate { get; set; }

        public decimal Value { get; set; }

        public DateTime LastModified { get; set; }

        public int LastModifiedUserId { get; set; }
    }
}
