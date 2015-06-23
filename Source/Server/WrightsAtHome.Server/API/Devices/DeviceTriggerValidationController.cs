using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using WrightsAtHome.Server.API.Devices.Model;
using WrightsAtHome.Server.Domain.Services.Trigger.Validator;

namespace WrightsAtHome.Server.API.Devices
{
    [RoutePrefix("api/devices")]
    public class DeviceTriggerValidationController : ApiController
    {
        private readonly ITriggerValidator triggerValidator;

        public DeviceTriggerValidationController(ITriggerValidator triggerValidator)
        {
            this.triggerValidator = triggerValidator;
        }

        [HttpPost]
        [Route("triggerValidation")]
        [ResponseType(typeof(TriggerValidationDto))]
        public IHttpActionResult ValidateTrigger([FromBody] TriggerValidationRequestDto request)
        {
            if (ModelState.IsValid)
            {
                return Ok(Mapper.Map<TriggerValidationDto>(
                            triggerValidator.ValidateTrigger(request.TriggerText)));
            }

            return BadRequest(ModelState);
        }
    }
}