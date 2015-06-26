using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class SensorType : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Sensor> Sensors { get; set; }

        public DateTime LastModified { get; set; }

        public int LastModifiedUserId { get; set; }

        public SensorType()
        {
            Sensors = new List<Sensor>();
        }
    }
}
