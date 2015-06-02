using System.Collections.Generic;

namespace WrightsAtHome.Server.Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string ImageName { get; set; }
        
        public List<DeviceState> PossibleStates { get; private set; }

        public string StartTriggerText { get; set; }
        
        public string EndTriggerText { get; set; }

        public List<DeviceStateChange> StateChanges { get; set; }
        
        public Device()
        {
            PossibleStates = new List<DeviceState>();
        }

        public Device(params DeviceState[] possibleStates)
        {
            PossibleStates = new List<DeviceState>(possibleStates);
        }
    }
}
