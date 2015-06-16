using System;
using System.ComponentModel.DataAnnotations;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class DeviceStateChange : IBaseEntity
    {
        public int Id { get; set; }

        public Device Device { get; set; }

        public DateTime AppliedDate { get; set; }

        public bool WasOverride { get; set; }

        public DeviceState BeforeState { get; set; }

        public DeviceState AfterState { get; set; }

        public DeviceTrigger FromTrigger { get; set; }

        [ConcurrencyCheck]
        public DateTime LastModified { get; set; }

        public int LastModifiedUserId { get; set; }
    }
}
