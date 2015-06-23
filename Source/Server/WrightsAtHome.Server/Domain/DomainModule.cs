using Autofac;
using WrightsAtHome.Server.Domain.Services;
using WrightsAtHome.Server.Domain.Services.Devices;
using WrightsAtHome.Server.Domain.Services.Devices.Internal;
using WrightsAtHome.Server.Domain.Services.Jobs;
using WrightsAtHome.Server.Domain.Services.Sensors;
using WrightsAtHome.Server.Domain.Services.Trigger;
using WrightsAtHome.Server.Domain.Services.Trigger.Compiler;
using WrightsAtHome.Server.Domain.Services.Trigger.Validator;

namespace WrightsAtHome.Server.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DateTimeHelpers>().As<IDateTimeHelpers>();
            builder.RegisterType<SensorCache>().As<ISensorCache>().SingleInstance();
            builder.RegisterType<TriggerHelpers>().As<ITriggerHelpers>().InstancePerLifetimeScope();
            builder.RegisterType<TriggerCompiler>().As<ITriggerCompiler>().InstancePerLifetimeScope();
            builder.RegisterType<DeviceTriggerCompiler>().As<IDeviceTriggerCompiler>().InstancePerLifetimeScope();
            builder.RegisterType<SensorService>().As<ISensorService>().InstancePerLifetimeScope();
            builder.RegisterType<DeviceStateService>().As<IDeviceStateService>().InstancePerLifetimeScope();
            builder.RegisterType<DeviceTriggerProcessor>().As<IDeviceTriggerProcessor>().InstancePerLifetimeScope();
            builder.RegisterType<DeviceTriggerValidationService>().As<IDeviceTriggerValidationService>().InstancePerLifetimeScope();
            builder.RegisterType<TriggerValidator>().As<ITriggerValidator>().InstancePerLifetimeScope();
            builder.RegisterType<DeviceTriggerEventService>().As<IDeviceTriggerEventService>().InstancePerLifetimeScope();
            builder.RegisterType<DeviceService>().As<IDeviceService>().InstancePerLifetimeScope();
            
            builder.RegisterType<DeviceTriggerJob>().As<IDeviceTriggerJob>().InstancePerLifetimeScope();
            builder.RegisterType<SensorReadingJob>().As<ISensorReadJob>().InstancePerLifetimeScope();
        }
    }
}