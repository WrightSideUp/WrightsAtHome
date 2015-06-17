using System.Linq;
using NLog;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain.Entities;

namespace WrightsAtHome.Server.Domain.Services.Devices.Internal
{
    public interface IDeviceStateService
    {
        // Returns current state of device
        DeviceState GetCurrentDeviceState(int deviceId);

        // Change the state of the device if toState is different from Current State
        void ChangeDeviceState(Device device, DeviceState toState);
    }

    public class DeviceStateService : IDeviceStateService
    {
        private readonly IAtHomeDbContext dbContext;
        private readonly Logger logger;

        private DeviceStateService(IAtHomeDbContext dbContext)
        {
            this.dbContext = dbContext;
            logger = LogManager.GetCurrentClassLogger();
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
                logger.Info("Changing State for Device {0} from {1} to {2}", device.Name, currentState.Name, toState.Name);
                // TODO: Change It!
            }
        }
    }
}