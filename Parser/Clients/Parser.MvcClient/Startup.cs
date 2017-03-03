using Microsoft.Owin;
using Microsoft.Owin.Cors;

using Owin;

[assembly: OwinStartupAttribute(typeof(Parser.MvcClient.Startup))]
namespace Parser.MvcClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var hubConfiguration = new HubConfiguration();
            //hubConfiguration.Resolver = new ParserHubConfigurationDependencyResolver(NinjectWebCommon.Kernel);

            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
