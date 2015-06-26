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
        private readonly IMappingEngine mapper;

        public DeviceTriggerValidationController(ITriggerValidator triggerValidator, IMappingEngine mapper)
        {
            this.triggerValidator = triggerValidator;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("triggerValidation")]
        [ResponseType(typeof(TriggerValidationDto))]
        public IHttpActionResult ValidateTrigger(TriggerValidationRequestDto request)
        {
            if (ModelState.IsValid)
            {
                return Ok(mapper.Map<TriggerValidationDto>(
                            triggerValidator.ValidateTrigger(request.TriggerText)));
            }

            return BadRequest(ModelState);
        }
    }
}