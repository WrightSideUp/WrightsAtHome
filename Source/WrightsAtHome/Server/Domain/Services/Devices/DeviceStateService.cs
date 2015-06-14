using System.Linq;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.Domain.Services.Devices
{
    public interface IDeviceStateService
    {
        DeviceState GetCurrentDeviceState(int deviceId);

        void ChangeDeviceState(Device device, DeviceState toState);
    }
    
    public class DeviceStateService : IDeviceStateService
    {
        private readonly IAtHomeDbContext dbContext;
        
        private DeviceStateService(IAtHomeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DeviceState GetCurrentDeviceState(int deviceId)
        {
            return dbContext.DeviceStates.First();
        }

        public void ChangeDeviceState(Device device, DeviceState toState)
        {
            var currentState = GetCurrentDeviceState(device.Id);

            // See if we need to change state of the device
            if (toState.Id != currentState.Id)
            {
                // Change it!
            }
        }
    }
}