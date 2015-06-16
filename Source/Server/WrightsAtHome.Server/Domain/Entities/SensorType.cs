using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class SensorType : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Sensor> Sensors { get; set; }

        [ConcurrencyCheck]
        public DateTime LastModified { get; set; }

        public int LastModifiedUserId { get; set; }
    }
}
