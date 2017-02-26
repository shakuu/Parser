using Microsoft.Owin;
using Microsoft.Owin.Cors;

using Owin;

[assembly: OwinStartupAttribute(typeof(Clients.MvcClient.Startup))]
namespace Clients.MvcClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();

            ConfigureAuth(app);
        }
    }
}
