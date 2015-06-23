using System.Threading.Tasks;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Devices.Internal;

namespace WrightsAtHome.Server.Domain.Services.Devices
{
    public interface IDeviceService
    {
        string GetNextTriggerEvent(int deviceId);

        void ProcessTriggers(int deviceId);

        DeviceState GetCurrentDeviceState(int deviceId);

        Task ChangeDeviceStateAsync(int deviceId, int toStateId);
    }

    public class DeviceService : IDeviceService
    {
        private readonly IDeviceTriggerEventService eventService;
        private readonly IDeviceTriggerProcessor triggerProcessor;
        private readonly IDeviceStateService stateService;

        public DeviceService(
            IDeviceTriggerEventService eventService, 
            IDeviceTriggerProcessor triggerProcessor,
            IDeviceStateService stateService)

        {
            this.eventService = eventService;
            this.triggerProcessor = triggerProcessor;
            this.stateService = stateService;
        }

        public string GetNextTriggerEvent(int deviceId)
        {
            return eventService.GetNextTriggerEvent(deviceId);
        }

        public void ProcessTriggers(int deviceId)
        {
            triggerProcessor.ProcessTriggers(deviceId);
        }

        public DeviceState GetCurrentDeviceState(int deviceId)
        {
            return stateService.GetCurrentDeviceState(deviceId);
        }

        public async Task ChangeDeviceStateAsync(int deviceId, int toStateId)
        {
            await stateService.ChangeDeviceStateAsync(deviceId, toStateId);
        }
    }
}