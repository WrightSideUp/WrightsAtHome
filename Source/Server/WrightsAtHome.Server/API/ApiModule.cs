using Autofac;
using AutoMapper;
using WrightsAtHome.Server.API.Devices.Model;


namespace WrightsAtHome.Server.API
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => Mapper.Engine).As<IMappingEngine>().SingleInstance();
            builder.RegisterType<NextEventResolver>();
            builder.RegisterType<CurrentStateResolver>();
        }
    }
}