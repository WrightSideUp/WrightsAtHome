using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using WrightsAtHome.Server.API.Common;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.API.Devices.Model
{
    [RoutePrefix("api/devices")]
    public class DeviceScheduleController : BaseApiController
    {
        public DeviceScheduleController(IAtHomeDbContext dbContext)
            : base(dbContext)
        {
        }

        [HttpGet]
        [Route("{deviceId:int}/schedule")]
        [ResponseType(typeof (DeviceTrigger[]))]
        public async Task<IHttpActionResult> GetTriggers(int deviceId)
        {
            var fetch = await FetchDevice(deviceId);

            return fetch.Result == null ? 
                fetch.HttpResult : 
                Ok(Mapper.Map<DeviceTriggerDto[]>(fetch.Result.Triggers));
        }

        [HttpGet]
        [Route("{deviceId:int}/schedule/{triggerId:int}")]
        [ResponseType(typeof(DeviceTrigger))]
        public async Task<IHttpActionResult> GetTrigger(int deviceId, int triggerId)
        {
            var fetch = await FetchTrigger(deviceId, triggerId);

            if (fetch.Result == null)
            {
                return fetch.HttpResult;
            }

            return Ok(Mapper.Map<DeviceTriggerDto>(fetch.Result));
        }

        
        [HttpPut]
        [Route("{id:int}/schedule")]
        public async Task<IHttpActionResult> UpdateTrigger(int deviceId, DeviceTriggerDto request)
        {
            var triggerFetch = await FetchTrigger(deviceId, request.Id);

            if (triggerFetch.Result == null)
            {
                return triggerFetch.HttpResult;
            }

            var stateFetch = await FetchState(request.ToStateId);

            if (stateFetch.Result == null)
            {
                return stateFetch.HttpResult;
            }

            var trigger = triggerFetch.Result;
            
            trigger.IsActive = request.IsActive;
            trigger.Sequence = request.Sequence;
            trigger.ToState = stateFetch.Result;
            trigger.TriggerText = request.TriggerText;

            return await TryCrudOperation();
        }

        [HttpPost]
        [Route("{id:int}/schedule")]
        [ResponseType(typeof(DeviceTriggerDto))]
        public async Task<IHttpActionResult> CreateTrigger(int deviceId, DeviceTriggerDto request)
        {
            var fetch = await FetchDevice(deviceId);

            if (fetch.Result == null)
            {
                return fetch.HttpResult;
            }

            var stateFetch = await FetchState(request.ToStateId);

            if (stateFetch.Result == null)
            {
                return stateFetch.HttpResult;
            }

            var newTrigger = new DeviceTrigger
            {
                IsActive = request.IsActive,
                Sequence = request.Sequence,
                ToState = stateFetch.Result,
                TriggerText = request.TriggerText,
            };
            
            fetch.Result.Triggers.Add(newTrigger);

            try
            {
                int numChanges = await dbContext.SaveChangesAsync();

                if (numChanges >= 1)
                {
                    return Created(Request.RequestUri + newTrigger.Id.ToString(), Mapper.Map<DeviceTriggerDto>(newTrigger));
                }

                return InternalServerError("No records created.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{deviceId:int}/schedule/{triggerId:int}")]
        public async Task<IHttpActionResult> DeleteTrigger(int deviceId, int triggerId)
        {
            var fetch = await FetchDevice(deviceId);

            if (fetch.Result == null)
            {
                return fetch.HttpResult;
            }

            var trigger = fetch.Result.Triggers.SingleOrDefault(t => t.Id == triggerId);

            if (trigger == null)
            {
                return NotFound("No Schedule with Id {0} exists for Device {1}", triggerId, deviceId);
            }

            fetch.Result.Triggers.Remove(trigger);

            try
            {
                int numChanges = await dbContext.SaveChangesAsync();

                if (numChanges >= 1)
                {
                    return NoContent();
                }

                return InternalServerError("No records deleted.");
            }
            catch (DBConcurrencyException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private async Task<FetchResult<Device>> FetchDevice(int deviceId)
        {
            var result = new FetchResult<Device>
            {
                Result = await dbContext.Devices.Include(d => d.Triggers)
                    .Include(d => d.Triggers.Select(t => t.ToState))
                    .SingleOrDefaultAsync(d => d.Id == deviceId)
            };


            if (result.Result == null)
            {
                result.HttpResult = NotFound("No Device with Id '{0}' exists.", deviceId);
            }

            return result;
        }

        private async Task<FetchResult<DeviceTrigger>> FetchTrigger(int deviceId, int triggerId)
        {
            var result = new FetchResult<DeviceTrigger>();

            var deviceResult = await FetchDevice(deviceId);

            if (deviceResult.Result == null)
            {
                result.HttpResult = deviceResult.HttpResult;
                return result;
            }

            result.Result = deviceResult.Result.Triggers.SingleOrDefault(t => t.Id == triggerId);

            if (result.Result == null)
            {
                result.HttpResult = NotFound("No Schedule with Id '{0}' exists for Device '{1}'", triggerId, deviceId);
            }

            return result;
        }

        private async Task<FetchResult<DeviceState>> FetchState(int stateId)
        {
            var result = new FetchResult<DeviceState>
            {
                Result = await dbContext.DeviceStates
                    .SingleOrDefaultAsync(s => s.Id == stateId)
            };

            if (result.Result == null)
            {
                result.HttpResult = NotFound("No Device State with Id '{0}' exists.", stateId);
            }

            return result;
        }

        private async Task<IHttpActionResult> TryCrudOperation()
        {
            try
            {
                int numChanges = await dbContext.SaveChangesAsync();

                if (numChanges >= 1)
                {
                    return Ok();
                }

                return InternalServerError("No records updated.");
            }
            catch (DBConcurrencyException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}