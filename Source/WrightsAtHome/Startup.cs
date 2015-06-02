using System.Diagnostics;
using Owin;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using WrightsAtHome.Server.DataAccess;
using WrightsAtHome.Server.Domain;

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
            // Setup Http Routing
            //-----------------------------------
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
