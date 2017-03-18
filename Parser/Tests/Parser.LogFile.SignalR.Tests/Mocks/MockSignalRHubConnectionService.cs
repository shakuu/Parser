using System.Collections.Generic;
using Parser.LogFile.SignalR.Contracts;
using Parser.LogFile.SignalR.Factories;
using Parser.LogFile.SignalR.Services;

namespace Parser.LogFile.SignalR.Tests.Mocks
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
