using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class DeviceState : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Sequence { get; set; }

        public IList<Device> Devices { get; set; }

        public bool IsTransitional { get; set; }

        public DateTime LastModified { get; set; }

        public int LastModifiedUserId { get; set; }

        public DeviceState()
        {
            Devices = new List<Device>();
        }
    }
}
