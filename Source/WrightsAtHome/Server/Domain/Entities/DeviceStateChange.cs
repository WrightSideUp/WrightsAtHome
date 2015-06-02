using System;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class DeviceStateChange
    {
        public int Id { get; set; }

        public Device Device { get; set; }

        public DateTime AppliedDate { get; set; }

        public bool WasOverride { get; set; }

        public DeviceState BeforeState { get; set; }

        public DeviceState AfterState { get; set; }
    }
}
