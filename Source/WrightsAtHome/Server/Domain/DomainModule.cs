using Autofac;
using WrightsAtHome.Server.Domain.Services.Devices;
using WrightsAtHome.Server.Domain.Services.Jobs;
using WrightsAtHome.Server.Domain.Services.Sensors;
using WrightsAtHome.Server.Domain.Services.Trigger;
using WrightsAtHome.Server.Domain.Services.Trigger.Parser;

namespace WrightsAtHome.Server.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TriggerHelpers>().As<ITriggerHelpers>().InstancePerLifetimeScope();
            builder.RegisterType<TriggerCompiler>().As<ITriggerCompiler>().InstancePerLifetimeScope();
            builder.RegisterType<SensorService>().As<ISensorService>().InstancePerLifetimeScope();
            builder.RegisterType<DeviceTriggerService>().As<IDeviceTriggerService>().InstancePerLifetimeScope();
            builder.RegisterType<DeviceStateService>().As<IDeviceStateService>().InstancePerLifetimeScope();

            builder.RegisterType<TriggerManager>().As<ITriggerManager>().SingleInstance();
            builder.RegisterType<SensorManager>().As<ISensorManager>().SingleInstance();
        }
    }
}