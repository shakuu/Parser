using Microsoft.AspNet.SignalR.Client;

using Parser.SignalR.Contracts;

namespace Parser.SignalR.Factories
{
    public interface IHubProxyProviderFactory
    {
        IHubProxyProvider CreateHubProxyProvider(IHubProxy hubProxy);
    }
}
