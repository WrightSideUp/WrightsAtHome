using System;
using System.Data.Entity.ModelConfiguration;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using WrightsAtHome.Server.API.Common;
using WrightsAtHome.Server.API.Devices.Model;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Devices;

namespace WrightsAtHome.Server.API.Devices
{
    [RoutePrefix("api/devices")]
    public class DeviceScheduleController : BaseApiController
    {
        private readonly IDeviceScheduleService scheduleService;
        private readonly IMappingEngine mapper;

        public DeviceScheduleController(IAtHomeDbContext dbContext,
                                        IDeviceScheduleService scheduleService,
                                        IMappingEngine mapper)
            : base(dbContext)
        {
            this.scheduleService = scheduleService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{deviceId:int}/schedule")]
        [ResponseType(typeof (DeviceTriggerDto[]))]
        public async Task<IHttpActionResult> GetTriggers(int deviceId)
        {
            return Ok(
                mapper.Map<DeviceTriggerDto[]>(
                    await scheduleService.GetTriggers(deviceId)));
        }

        [HttpGet]
        [Route("{deviceId:int}/schedule/{triggerId:int}", Name="GetDeviceTriggerById")]
        [ResponseType(typeof(DeviceTrigger))]
        public async Task<IHttpActionResult> GetTrigger(int deviceId, int triggerId)
        {
            return Ok(
                mapper.Map<DeviceTriggerDto>(
                    await scheduleService.GetTrigger(deviceId, triggerId)));
        }

        [HttpPut]
        [Route("{deviceId:int}/schedule/{triggerId:int}")]
        public async Task<IHttpActionResult> UpdateTrigger(int deviceId, int triggerId, DeviceTriggerDto request)
        {
            await scheduleService.UpdateTrigger(deviceId, new DeviceTrigger
            {
                Id = triggerId,
                IsActive = request.IsActive,
                Sequence = request.Sequence,
                ToState = new DeviceState {Id = request.ToStateId},
                TriggerText = request.TriggerText,
                LastModified = request.LastModified
            });
            
            return Ok();
        }

        [HttpPost]
        [Route("{deviceId:int}/schedule")]
        [ResponseType(typeof(DeviceTriggerDto))]
        public async Task<IHttpActionResult> CreateTrigger(int deviceId, DeviceTriggerDto request)
        {
            var newTrigger = await scheduleService.CreateTrigger(deviceId, new DeviceTrigger
            {
                Id = request.Id,
                IsActive = request.IsActive,
                Sequence = request.Sequence,
                ToState = new DeviceState {Id = request.ToStateId},
                TriggerText = request.TriggerText,
            });

            var uriString = Url.Link("GetDeviceTriggerById", new {deviceId, triggerId = newTrigger.Id});

            return Created(new Uri(uriString), mapper.Map<DeviceTriggerDto>(newTrigger));
        }

        [HttpDelete]
        [Route("{deviceId:int}/schedule/{triggerId:int}")]
        public async Task<IHttpActionResult> DeleteTrigger(int deviceId, int triggerId)
        {
            await scheduleService.DeleteTrigger(deviceId, triggerId);
            return NoContent();
        }
    }
}