using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.API.Common;
using WrightsAtHome.Server.API.Devices.Model;

namespace WrightsAtHome.Server.API.Devices
{
    [RoutePrefix("api/devices")]
    public class DevicesController : BaseApiController
    {
        private readonly IMappingEngine mapper;

        public DevicesController(IAtHomeDbContext dbContext, IMappingEngine mapper) 
            : base(dbContext)
        {
            this.mapper = mapper;
        }
        
        // GET: api/devices
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(DeviceDto[]))]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(
                        (await dbContext.Devices
                            .Include(d => d.PossibleStates)
                            .AsNoTracking()
                            .OrderBy(d => d.Sequence)
                            .ToArrayAsync()).Select(d => mapper.Map<DeviceDto>(d))
                    );
        }

        // GET api/devices/5
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(DeviceDto))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await dbContext.Devices
                .Include(d => d.PossibleStates)
                .AsNoTracking()
                .SingleOrDefaultAsync(d => d.Id == id);

            if (result == null)
            {
                return NotFound("No Device with Id '{0}' exists", id);
            }

            return Ok(mapper.Map<DeviceDto>(result));
        }

        // GET api/devices/5/details
        [HttpGet]
        [Route("{id:int}/details")]
        [ResponseType(typeof(DeviceDetailsDto))]
        public async Task<IHttpActionResult> Details(int id)
        {
            var device = await dbContext.Devices
                        .Include(d => d.Triggers.Select(t => t.ToState))
                        .Include(d => d.PossibleStates)
                        .SingleOrDefaultAsync(d => d.Id == id);

            if (device == null)
            {
                return NotFound("No Device with Id '{0}' exists", id);
            }

            dbContext.Entry(device)
                .Collection(d => d.StateChanges)
                .Query()
                .Include(s => s.BeforeState)
                .Include(s => s.AfterState)
                .OrderByDescending(s => s.AppliedDate)
                .Take(1)
                .Load();

            return Ok(mapper.Map<DeviceDetailsDto>(device));
        }
    }
}
