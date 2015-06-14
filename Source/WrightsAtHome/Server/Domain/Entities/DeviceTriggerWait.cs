using System;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class DeviceTriggerWait : IBaseEntity
    {
        public int Id { get; set; }

        public DeviceTrigger AfterTrigger { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime LastModified { get; set; }

        public int LastModifiedUserId { get; set; }
    }
}