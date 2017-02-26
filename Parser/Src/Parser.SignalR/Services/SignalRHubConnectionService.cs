using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.SignalR.Contracts;
using Parser.SignalR.Factories;

namespace Parser.SignalR.Services
{
    public class SignalRHubConnectionService : ISignalRHubConnectionService
    {
        private const string HubConnectionUrl = "http://localhost:52589";

        private readonly IHubConnectionProvider hubConnectionProvider;
        private readonly IHubProxyProviderFactory hubProxyProviderFactory;

        private readonly IDictionary<string, IHubProxyProvider> hubProxyProviders;

        public SignalRHubConnectionService(IHubConnectionProviderFactory hubConnectionProviderFactory, IHubProxyProviderFactory hubProxyProviderFactory)
        {
            Guard.WhenArgument(hubConnectionProviderFactory, nameof(IHubConnectionProviderFactory)).IsNull().Throw();
            Guard.WhenArgument(hubProxyProviderFactory, nameof(IHubProxyProviderFactory)).IsNull().Throw();

            this.hubConnectionProvider = hubConnectionProviderFactory.CreateHubConnectionProvider(SignalRHubConnectionService.HubConnectionUrl);
            this.hubProxyProviderFactory = hubProxyProviderFactory;

            this.hubProxyProviders = new Dictionary<string, IHubProxyProvider>();
        }

        public IHubProxyProvider GetHubProxyProvider(string hubName)
        {
            if (this.hubProxyProviders.ContainsKey(hubName) == false)
            {
                var newHubProxy = this.hubConnectionProvider.CreateHubProxy(hubName);
                var newHubProxyProvider = this.hubProxyProviderFactory.CreateHubProxyProvider(newHubProxy);

                this.hubProxyProviders.Add(hubName, newHubProxyProvider);
            }

            return this.hubProxyProviders[hubName];
        }
    }
}
