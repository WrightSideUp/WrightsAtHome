using System.Data.Entity.Core;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using WrightsAtHome.Server.API.Common;
using WrightsAtHome.Server.API.Devices.Model;
using WrightsAtHome.Server.Domain.Services.Devices;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WrightsAtHome.Server.API.Devices
{
    [RoutePrefix("api/devices")]
    public class DeviceStateController : BaseApiController
    {
        private readonly IDeviceService deviceService;

        public DeviceStateController(IDeviceService deviceService) :
            base(null)
        {
            this.deviceService = deviceService;
        }

        [HttpGet]
        [Route("{id:int}/state")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(Mapper.Map<DeviceStateDto>(deviceService.GetCurrentDeviceState(id)));
            }
            catch (ObjectNotFoundException)
            {
                return NotFound("No Device with id {0} exists.", id);
            }
        }
        
        [HttpPost]
        [Route("{id:int}/state")]
        public async Task<IHttpActionResult> Post(int id, [FromBody]DeviceStateChangeDto changeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await deviceService.ChangeDeviceStateAsync(id, changeRequest.StateId);
                return Ok();
            }
            catch (ObjectNotFoundException)
            {
                return NotFound("No Device with id {0} exists.", id);
            }
        }
    }
}
