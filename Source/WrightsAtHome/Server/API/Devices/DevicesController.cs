using System.Collections.Generic;
using System.Web.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WrightsAtHome.Server.API.Devices
{
    [RoutePrefix("api/[controller]")]
    public class DevicesController : ApiController
    {
        private readonly List<DeviceInfo> data = new List<DeviceInfo>
        {
            new DeviceInfo { Id=1, Name = "Pool Light", IsOn = false, SmallImageUrl = "img/devices_small/PoolLight.png", LargeImageUrl = "img/devices_large/PoolLight.png", NextEvent = "Turn On at Dark" },
            new DeviceInfo { Id=2, Name = "Fountain", IsOn = true, SmallImageUrl = "img/devices_small/Fountain.png", LargeImageUrl = "img/devices_large/Fountain.png", NextEvent = "None" },
            new DeviceInfo { Id=3, Name="Landscape Lights - Front", IsOn = false, SmallImageUrl = "img/devices_small/LandscapeLights.png", LargeImageUrl="img/devices_large/LandscapeLights.png", NextEvent = "Turn Off at 11:00pm" },
            new DeviceInfo { Id=4, Name="Landscape Lights - Back", IsOn = true, SmallImageUrl = "img/devices_small/LandscapeLights.png", LargeImageUrl = "img/devices_large/LandscapeLights.png", NextEvent = "Turn Off at 12:00am" },
            new DeviceInfo { Id=5, Name="Pool Pump", IsOn = false, SmallImageUrl = "img/devices_small/PoolPump.png", LargeImageUrl = "img/devices_large/PoolPump.png", NextEvent = "Turn On at 1:00am" },
            new DeviceInfo { Id=6, Name="Pool Heater", IsOn = false, SmallImageUrl = "img/devices_small/PoolHeater.png", LargeImageUrl = "img/devices_large/PoolHeater.png", NextEvent = "Turn On when Pool 10deg < Air" },
            new DeviceInfo { Id=7, Name="Xmas Lights", IsOn = false, SmallImageUrl = "img/devices_small/XmasLights.png", LargeImageUrl = "img/devices_large/XmasLights.png", NextEvent = "None" },
        };

        
        // GET: api/values
        public IEnumerable<DeviceInfo> Get()
        {
            return data;
        }
    }
}
