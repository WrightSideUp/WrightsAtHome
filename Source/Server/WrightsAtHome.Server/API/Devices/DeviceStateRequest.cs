namespace WrightsAtHome.Server.API.Devices
{
    public class DeviceStateRequest
    {
        public int Id { get; set; }
        public bool DesiredState { get; set; }
    }
}
