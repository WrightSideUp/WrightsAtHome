using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;
using WrightsAtHome.Server.API.Common;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Sensors;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WrightsAtHome.Server.API.Sensors
{
    [RoutePrefix("api/[controller]")]
    public class SensorReadingsController : ApiController
    {
        private readonly IAtHomeDbContext dbContext;
        private readonly ISensorService sensorService;

        public SensorReadingsController(IAtHomeDbContext dbContext, ISensorService sensorService)
        {
            this.dbContext = dbContext;
            this.sensorService = sensorService;
        }

        // GET: api/sensorreadings
        public async Task<IEnumerable<SensorReadingInfo>> Get()
        {
            var sensors = await dbContext.Sensors.Include(s => s.SensorType).ToListAsync();

            return sensors.Select(GetSensorReadingProjetionForSensor);
        }

        // GET api/sensors/5
        public async Task<SensorReadingInfo> Get(int id)
        {
            var sensor = await dbContext.Sensors.Include(s => s.SensorType).SingleOrDefaultAsync(s => s.Id == id);

            if (sensor == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No Sendor with Id: {0}", id)),
                    ReasonPhrase = "Sensor Id Not Found"
                });
            }

            return GetSensorReadingProjetionForSensor(sensor);
        }

        private SensorReadingInfo GetSensorReadingProjetionForSensor(Sensor s)
        {
            return new SensorReadingInfo
            {
                Id = s.Id,
                Name = s.Name,
                SensorType = s.SensorType.Name,
                LargeImageUrl = this.DeviceImageUrlLarge(s.ImageName),
                SmallImageUrl = this.DeviceImageUrlSmall(s.ImageName),
                Reading = sensorService.GetCurrentSensorReading(s.Id)
            };
        }
    }
}
