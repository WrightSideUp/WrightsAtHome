using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class Sensor : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Sequence { get; set; }
        
        public string ImageName { get; set; }
        
        public SensorType SensorType { get; set; }
        
        public TimeSpan ReadInterval { get; set; }

        public IList<SensorReading> Readings { get; set; }

        [ConcurrencyCheck]
        public DateTime LastModified { get; set; }

        public int LastModifiedUserId { get; set; }
    }
}
