using Microsoft.AspNet.SignalR.Client;
using Parser.LogFile.SignalR.Contracts;

namespace Parser.LogFile.SignalR.Factories
{
    public interface IHubProxyProviderFactory
    {
        IHubProxyProvider CreateHubProxyProvider(IHubProxy hubProxy);
    }
}
