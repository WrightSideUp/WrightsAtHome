
namespace WrightsAtHome.Server.API.Devices.Model
{
    public class DeviceDetailsDto : DeviceDto
    {
        public DeviceTriggerDto[] Triggers { get; set; }

        public string LastStateChange { get; set; }
    }
}