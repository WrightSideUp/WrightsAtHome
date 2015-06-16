using Hangfire;
using Owin;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Config;
using NLog.Targets;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain;
using WrightsAtHome.Server.Domain.Services.Jobs;

namespace WrightsAtHome
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            
            //-----------------------------------
            // Setup DI Container
            //-----------------------------------
            var builder = new ContainerBuilder();

            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<DataAccessModule>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            app.UseAutofacMiddleware(container);
            
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //-----------------------------------
            // Setup Logging
            //-----------------------------------
            var logConfig = new LoggingConfiguration();

            var consoleTarg = new ColoredConsoleTarget();
            consoleTarg.Layout = "${logger}::${message}";
            logConfig.AddTarget("console", consoleTarg);
            
            var dbTarg = new DatabaseTarget();
            dbTarg.ConnectionStringName = "AtHomeDbContext";
            dbTarg.CommandText = "INSERT INTO Log (TimeStamp, Message, Exception, Level, Logger) VALUES(GETDATE(), @msg, @exception, @level, @logger)";
            dbTarg.Parameters.Add(new DatabaseParameterInfo("@msg", "${message}"));
            dbTarg.Parameters.Add(new DatabaseParameterInfo("@exception", "${exception}"));
            dbTarg.Parameters.Add(new DatabaseParameterInfo("@level", "${level}"));
            dbTarg.Parameters.Add(new DatabaseParameterInfo("@logger", "${logger}"));
            logConfig.AddTarget("db", dbTarg);

            var debugTarg = new DebugTarget();
            debugTarg.Layout = "${logger}::${message}";
            logConfig.AddTarget("debugger", debugTarg);

            var rule1 = new LoggingRule("*", LogLevel.Info, consoleTarg);
            logConfig.LoggingRules.Add(rule1);

            var rule2 = new LoggingRule("*", LogLevel.Info , dbTarg);
            logConfig.LoggingRules.Add(rule2);

            var rule3 = new LoggingRule("*", LogLevel.Info, debugTarg);
            logConfig.LoggingRules.Add(rule3);

            LogManager.ThrowExceptions = true;
            LogManager.Configuration = logConfig;

            //-----------------------------------
            // Setup Hangfire for background jobs
            //-----------------------------------
            Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("AtHomeDbContext");
            Hangfire.GlobalConfiguration.Configuration.UseAutofacActivator(container);
            app.UseHangfireServer();
            
            //---------------------------------------
            // Start the Trigger and Sensor jobs 
            //---------------------------------------
            RecurringJob.AddOrUpdate<ITriggerManager>(x => x.MonitorSchedules(), Cron.Minutely); // every minute
            RecurringJob.AddOrUpdate<ISensorManager>(x => x.GetSensorReadings(), "*/10 * * * *");    // every 10 minutes
            
            //-----------------------------------
            // Setup Http Routing
            //-----------------------------------
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //-----------------------------------
            // Setup JSON Formatting
            //-----------------------------------
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //-----------------------------------
            // Start Web API
            //-----------------------------------
            app.UseWebApi(config);
        }
    }
}
