
namespace WrightsAtHome.Server.API.Devices
{
    public class DeviceInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DeviceStateInfo[] PossibleStates { get; set; }

        public int CurrentStateId { get; set; }
        
        public string SmallImageUrl { get; set; }

        public string LargeImageUrl { get; set; }

        public string NextEvent { get; set; }
    }
}
