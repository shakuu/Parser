using System.Collections.Generic;

using Parser.SignalR.Contracts;
using Parser.SignalR.Factories;
using Parser.SignalR.Services;

namespace Parser.SignalR.Tests.Mocks
{
    internal class MockSignalRHubConnectionService : SignalRHubConnectionService, ISignalRHubConnectionService
    {
        internal MockSignalRHubConnectionService(IHubConnectionProviderFactory hubConnectionProviderFactory, IHubProxyProviderFactory hubProxyProviderFactory)
            : base(hubConnectionProviderFactory, hubProxyProviderFactory)
        {
        }

        internal new IDictionary<string, IHubProxyProvider> HubProxyProviders { get { return base.HubProxyProviders; } }
    }
}
