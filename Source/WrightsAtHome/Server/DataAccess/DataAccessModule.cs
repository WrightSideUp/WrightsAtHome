
using Autofac;

namespace WrightsAtHome.Server.DataAccess
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AtHomeDbContext>().As<IAtHomeDbContext>().InstancePerLifetimeScope();
        }
    }
}