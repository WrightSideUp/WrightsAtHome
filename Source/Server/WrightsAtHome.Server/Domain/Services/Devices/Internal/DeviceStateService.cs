using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Threading.Tasks;
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
        void ChangeDeviceState(Device device, DeviceState toState, DeviceTrigger trigger);

        // Change the state of a device if toState is different from Current State
        Task ChangeDeviceStateAsync(int deviceId, int toStateId);
    }

    public class DeviceStateService : IDeviceStateService
    {
        private readonly IAtHomeDbContext dbContext;
        private readonly Logger logger;

        public DeviceStateService(IAtHomeDbContext dbContext)
        {
            this.dbContext = dbContext;
            logger = LogManager.GetCurrentClassLogger();
        }

        public DeviceState GetCurrentDeviceState(int deviceId)
        {
            if (! dbContext.Devices.Any(d => d.Id == deviceId))
            {
                throw new ObjectNotFoundException(string.Format("No Device with Id {0} is defined.", deviceId));
            }

            // Temporarily look for last state change and return it if it exists,
            // Otherwise return On for even deviceId's and Off for odd.
            var lastChange =
                dbContext.DeviceStateChanges
                    .Include(sc => sc.AfterState)
                    .AsNoTracking()
                    .Where(sc => sc.Device.Id == deviceId)
                    .OrderByDescending(sc => sc.AppliedDate)
                    .Select(sc => sc.AfterState)
                    .FirstOrDefault();

            if (lastChange == null)
            {
                var device = dbContext.Devices.Include(d => d.PossibleStates).Single(d => d.Id == deviceId);

                return (deviceId%2 == 0)
                    ? device.PossibleStates[0]
                    : device.PossibleStates[1];
            }

            return lastChange;
        }

        public void ChangeDeviceState(Device device, DeviceState toState, DeviceTrigger trigger)
        {
            var currentState = GetCurrentDeviceState(device.Id);

            // See if we need to change state of the device
            if (toState.Id != currentState.Id)
            {
                logger.Info("Changing State for Device {0} from {1} to {2}", device.Name, currentState.Name, toState.Name);

                var beforeState = dbContext.DeviceStates.Single(s => s.Id == currentState.Id);
                
                device.StateChanges.Add(new DeviceStateChange
                {
                    AppliedDate = DateTime.Now,
                    AfterState = toState,
                    BeforeState = beforeState,
                    FromTrigger = trigger,
                    WasOverride = (trigger == null)
                });

                dbContext.SaveChanges();

                // TODO: Change It!
            }
        }

        public async Task ChangeDeviceStateAsync(int deviceId, int toStateId)
        {
            var device = await dbContext.Devices
                .Include(d => d.PossibleStates)
                .SingleOrDefaultAsync(d => d.Id == deviceId);

            if (device == null)
            {
                throw new ObjectNotFoundException(string.Format("Device with id {0} not found.", deviceId));
            }

            var toState = device.PossibleStates.SingleOrDefault(s => s.Id == toStateId);

            if (toState == null)
            {
                throw new ObjectNotFoundException(
                    string.Format("Device State with id {0} not found or is invalid for {1}.", toStateId, device.Name));
            }

            ChangeDeviceState(device, toState, null);
        }
    }
}