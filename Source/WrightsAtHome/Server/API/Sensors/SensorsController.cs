using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WrightsAtHome.Server.API.Sensors
{
    [RoutePrefix("api/[controller]")]
    public class SensorsController : ApiController
    {
        private readonly List<SensorReadingInfo> data = new List<SensorReadingInfo>
        {
            new SensorReadingInfo { Id = 1, Name = "Air Temp", SensorType = "Temperature", Reading = 77, SmallImageUrl = "img/devices_small/thermometer.png", LargeImageUrl = "img/devices_large/thermometer.png" },
            new SensorReadingInfo { Id = 2, Name = "Pool Temp", SensorType = "Temperature", Reading = 82, SmallImageUrl = "img/devices_small/thermometer.png", LargeImageUrl = "img/devices_large/thermometer.png" },
            new SensorReadingInfo { Id = 3, Name = "Light Level", SensorType = "Light", Reading = 1, SmallImageUrl = "img/devices_small/LightSensor.png", LargeImageUrl = "img/devices_large/LightSensor.png" },
        };

        // GET: api/sensors
        public IEnumerable<SensorReadingInfo> Get()
        {
            return data;
        }

        // GET api/sensors/5
        public SensorReadingInfo Get(int id)
        {
            return data.SingleOrDefault(d => d.Id == id);
        }
    }
}
