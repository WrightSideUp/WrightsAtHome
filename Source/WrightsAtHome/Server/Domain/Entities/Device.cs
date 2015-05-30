using System;
using System.Collections.Generic;

namespace WrightsAtHome.BackEnd.Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SmallImageUrl { get; set; }
        public string LargeImageUrl { get; set; }
        public List<DeviceState> PossibleStates { get; private set; }
        public DeviceState CurrentState { get; set; }
        public DeviceStateChange LastStateChange { get; set; }
        public string StartTriggerText { get; set; }
        public Func<bool> StartTrigger { get; set; } 
        public string StopTriggerText { get; set; }
        public Func<DateTime, bool> EndTrigger { get; set; }

        public Device()
        {
            PossibleStates = new List<DeviceState>();
        }
    }
}
