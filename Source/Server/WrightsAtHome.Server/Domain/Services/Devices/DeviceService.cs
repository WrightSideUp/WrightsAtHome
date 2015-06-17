using System.Threading.Tasks;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Devices.Internal;

namespace WrightsAtHome.Server.Domain.Services.Devices
{
    public interface IDeviceService
    {
        Task<string> GetNextTriggerEvent(int deviceId);

        void ProcessTriggers(int deviceId);

        DeviceState GetCurrentDeviceState(int deviceId);

        void ChangeDeviceState(Device device, DeviceState toState);

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

        public Task<string> GetNextTriggerEvent(int deviceId)
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

        public void ChangeDeviceState(Device device, DeviceState toState)
        {
            stateService.ChangeDeviceState(device, toState);
        }
    }
}