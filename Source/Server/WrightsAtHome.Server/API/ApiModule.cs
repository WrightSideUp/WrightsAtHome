using Autofac;
using AutoMapper;


namespace WrightsAtHome.Server.API
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MappingEngine>().As<IMappingEngine>().InstancePerLifetimeScope();
        }
    }
}