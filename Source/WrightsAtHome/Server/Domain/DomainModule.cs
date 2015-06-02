using Autofac;
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
        }
    }
}