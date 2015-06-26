using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Devices.Internal;

namespace WrightsAtHome.Server.Domain.Services.Devices
{
    public interface IDeviceScheduleService
    {
        Task<IList<DeviceTrigger>> GetTriggers(int deviceId);

        Task<DeviceTrigger> GetTrigger(int deviceId, int triggerId);

        Task<DeviceTrigger> UpdateTrigger(int deviceId, DeviceTrigger trigger);

        Task<DeviceTrigger> CreateTrigger(int deviceId, DeviceTrigger trigger);

        Task DeleteTrigger(int deviceId, int triggerId);
    }
    
    public class DeviceScheduleService : IDeviceScheduleService
    {
        private readonly IAtHomeDbContext dbContext;
        private readonly IDeviceTriggerValidationService validationService;

        public DeviceScheduleService(IAtHomeDbContext dbContext, IDeviceTriggerValidationService validationService)
        {
            this.dbContext = dbContext;
            this.validationService = validationService;
        }

        public async Task<IList<DeviceTrigger>> GetTriggers(int deviceId)
        {
            return (await FetchDevice(deviceId)).Triggers;
        }

        public async Task<DeviceTrigger> GetTrigger(int deviceId, int triggerId)
        {
            return await FetchTrigger(deviceId, triggerId);
        }

        public async Task<DeviceTrigger> UpdateTrigger(int deviceId, DeviceTrigger trigger)
        {
            var curTrigger = await FetchTrigger(deviceId, trigger.Id);

            var state = await FetchState(trigger.ToState.Id);
            
            curTrigger.IsActive = trigger.IsActive;
            curTrigger.Sequence = trigger.Sequence;
            curTrigger.ToState = state;
            curTrigger.TriggerText = trigger.TriggerText;
            curTrigger.LastModified = trigger.LastModified;

            ValidateDeviceTriggers(curTrigger.Device);

            var numRows = await dbContext.SaveChangesAsync();

            if (numRows == 0)
            {
                throw new Exception("No schedule was updated.");
            }

            return curTrigger;
        }

        public async Task<DeviceTrigger> CreateTrigger(int deviceId, DeviceTrigger trigger)
        {
            var device = await FetchDevice(deviceId);

            trigger.ToState = await FetchState(trigger.ToState.Id);
            
            device.Triggers.Add(trigger);

            ValidateDeviceTriggers(device);

            var numRows = await dbContext.SaveChangesAsync();

            if (numRows == 0)
            {
                throw new Exception("No schedule was created.");
            }

            return trigger;
        }

        public async Task DeleteTrigger(int deviceId, int triggerId)
        {
            //var t = new DeviceTrigger {Id = triggerId};
            //dbContext.DeviceTriggers.Attach(t);
            //dbContext.Entry(t).State = EntityState.Deleted;
            
            //await dbContext.SaveChangesAsync()
            
            var curTrigger = await FetchTrigger(deviceId, triggerId);

            var device = curTrigger.Device;

            device.Triggers.Remove(curTrigger);

            ValidateDeviceTriggers(device);
            
            dbContext.Entry(curTrigger).State = EntityState.Deleted;

            var numRows = await dbContext.SaveChangesAsync();

            if (numRows == 0)
            {
                throw new Exception("No schedule was deleted.");
            }
        }

        private async Task<Device> FetchDevice(int deviceId)
        {
            var device = await dbContext.Devices.Include(d => d.Triggers)
                .Include(d => d.Triggers.Select(t => t.ToState))
                .Include(d => d.PossibleStates)
                .SingleOrDefaultAsync(d => d.Id == deviceId);

            if (device == null)
            {
                throw new ObjectNotFoundException(string.Format("No Device with Id '{0}' exists.", deviceId));
            }

            return device;
        }

        private async Task<DeviceTrigger> FetchTrigger(int deviceId, int triggerId)
        {
            var device = await FetchDevice(deviceId);

            var trigger = device.Triggers.SingleOrDefault(t => t.Id == triggerId);

            if (trigger == null)
            {
                throw new ObjectNotFoundException(string.Format("No Schedule with Id '{0}' exists for Device '{1}'", triggerId, device.Name));
            }

            return trigger;
        }

        private async Task<DeviceState> FetchState(int stateId)
        {
            var state = await dbContext.DeviceStates
                .SingleOrDefaultAsync(s => s.Id == stateId);

            if (state == null)
            {
                throw new ObjectNotFoundException(String.Format("No Device State with Id '{0}' exists.", stateId));
            }

            return state;
        }

        private void ValidateDeviceTriggers(Device device)
        {
            var validInfo = validationService.ValidateTriggers(device);

            if (!validInfo.IsValid)
            {
                throw new DbEntityValidationException(validInfo.ErrorMessage);
            }
        }
    }
}