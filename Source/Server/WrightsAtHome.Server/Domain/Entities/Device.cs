using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class Device : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Sequence { get; set; }

        public string ImageName { get; set; }
        
        public IList<DeviceState> PossibleStates { get; private set; }

        public IList<DeviceTrigger> Triggers { get; set; }

        public IList<DeviceStateChange> StateChanges { get; set; }

        public DateTime LastModified { get; set; }

        public int LastModifiedUserId { get; set; }

        public Device()
        {
            PossibleStates = new List<DeviceState>();
            Triggers = new List<DeviceTrigger>();
            StateChanges =new List<DeviceStateChange>();
        }

        public Device(params DeviceState[] possibleStates)
        {
            PossibleStates = new List<DeviceState>(possibleStates);
            Triggers = new List<DeviceTrigger>();
            StateChanges = new List<DeviceStateChange>();
        }
    }
}
