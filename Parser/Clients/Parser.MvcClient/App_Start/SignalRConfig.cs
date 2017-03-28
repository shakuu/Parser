using Microsoft.AspNet.SignalR;

using Parser.MvcClient.App_Start;
using Parser.MvcClient.App_Start.SignalRHubDependencyResolver;

namespace Parser.MvcClient
{
    public class SignalRConfig
    {
        public static void InitializeSignalRResolver()
        {
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.Resolver = new ParserHubConfigurationDependencyResolver(NinjectWebCommon.Kernel);
        }
    }
}