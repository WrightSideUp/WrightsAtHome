using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using WrightsAtHome.Server.API.Sensors.Model;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Sensors;


namespace WrightsAtHome.Server.API.Sensors
{
    [RoutePrefix("api/sensors")]
    public class SensorReadingsController : ApiController
    {
        private readonly IAtHomeDbContext dbContext;
        private readonly ISensorService sensorService;

        public SensorReadingsController(IAtHomeDbContext dbContext, ISensorService sensorService)
        {
            this.dbContext = dbContext;
            this.sensorService = sensorService;
        }

        // GET api/sensorreadings
        [Route("")]
        [ResponseType(typeof(SensorReadingDto[]))]
        public async Task<IHttpActionResult> Get()
        {
            var sensors = await dbContext.Sensors
                .Include(s => s.SensorType)
                .AsNoTracking()
                .ToListAsync();

            return Ok(sensors.Select(AsDto).ToArray());
        }

        // GET api/sensorreadings/5
        [Route("{id:int}")]
        [ResponseType(typeof(SensorReadingDto))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var sensor = await dbContext.Sensors
                .Include(s => s.SensorType)
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == id);

            if (sensor == null)
            {
                return NotFound();
            }

            return Ok(AsDto(sensor));
        }

        private SensorReadingDto AsDto(Sensor s)
        {
            var dto = Mapper.Map<SensorReadingDto>(s);
            dto.Reading = sensorService.GetCurrentSensorReading(s.Id);
            return dto;
        }
    }
}
