using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.API.Common;
using WrightsAtHome.Server.API.Devices.Model;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Devices;

namespace WrightsAtHome.Server.API.Devices
{
    [RoutePrefix("api/devices")]
    public class DevicesController : BaseApiController
    {
        private readonly IDeviceService deviceService;

        public DevicesController(IAtHomeDbContext dbContext, IDeviceService deviceService) 
            : base(dbContext)
        {
            this.deviceService = deviceService;
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
                            .ToArrayAsync()).Select(d => MapServiceProperties(Mapper.Map<DeviceDto>(d)))
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

            return Ok(MapServiceProperties(Mapper.Map<DeviceDto>(result)));
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
                        .AsNoTracking()
                        .SingleOrDefaultAsync(d => d.Id == id);

            if (device == null)
            {
                return NotFound("No Device with Id '{0}' exists", id);
            }

            var lastChange = await dbContext.Database.SqlQuery<DeviceStateChange>(
                "SELECT TOP (1) * FROM dbo.DeviceStateChange WHERE DeviceId = @p0 ORDER BY AppliedDate DESC",
                device.Id).SingleOrDefaultAsync();

            if (lastChange != null)
            {
                device.StateChanges.Add(lastChange);
            }

            return Ok(MapServiceProperties(Mapper.Map<DeviceDetailsDto>(device)));
        }

        private DeviceDto MapServiceProperties(DeviceDto dto) 
        {
            dto.NextEvent = deviceService.GetNextTriggerEvent(dto.Id);
            dto.CurrentStateId = deviceService.GetCurrentDeviceState(dto.Id).Id;
            return dto;
        }
    }
}
