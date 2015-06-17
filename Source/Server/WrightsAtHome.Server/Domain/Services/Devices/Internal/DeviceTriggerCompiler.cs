using System.Collections.Generic;
using System.Linq;
using WrightsAtHome.Server.Domain.Entities;
using WrightsAtHome.Server.Domain.Services.Trigger;

namespace WrightsAtHome.Server.Domain.Services.Devices.Internal
{
    public class TriggerContext
    {
        public TriggerInfo Primary { get; set; }

        public TriggerInfo Dependent { get; set; }
    }

    public interface IDeviceTriggerCompiler
    {
        // Compile all triggers for device and put them into context map which ties 
        // any After triggers to their corresopnding When or At
        IList<TriggerContext> CompileTriggers(Device device);
    }

    public class DeviceTriggerCompiler : IDeviceTriggerCompiler
    {
        private readonly ITriggerCompiler triggerCompiler;

        public DeviceTriggerCompiler(ITriggerCompiler triggerCompiler)
        {
            this.triggerCompiler = triggerCompiler;
        }

        public IList<TriggerContext> CompileTriggers(Device device)
        {
            // Create list of all compiled triggers
            var result =
                (from trigger in device.Triggers
                 where trigger.IsActive
                 orderby trigger.Sequence
                 select new TriggerContext
                 {
                     Primary = triggerCompiler.CompileTrigger(trigger)
                 })
            .ToList();


            // Loop through list and create context for each trigger (assigning dependent triggers for After triggers)
            for (int i = 0; i < result.Count; i++)
            {
                if (i < result.Count - 1)
                    if (result[i + 1].Primary.TriggerType == TriggerType.After)
                    {
                        result[i].Dependent = result[i + 1].Primary;
                    }
            }

            return result;
        }
    }
}