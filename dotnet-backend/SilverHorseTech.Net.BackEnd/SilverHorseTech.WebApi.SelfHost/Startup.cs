using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Reflection;

namespace SilverHorseTech.WebApi.SelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Replace(typeof(IAssembliesResolver), new AssembliesResolver());
            config.MessageHandlers.Add(new AuthorizationHeaderHandler());

            app.UseWebApi(config);
        }
    }

    class AssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> assemblies = base.GetAssemblies();
            var apiAssembly = Assembly.LoadFrom(@"SilverHorseTech.WebApi.dll");
            assemblies.Add(apiAssembly);
            return assemblies;
        }
    }
}
