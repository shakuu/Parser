using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.LogFile.SignalR.Contracts;
using Parser.LogFile.SignalR.Factories;

namespace Parser.LogFile.SignalR.Services
{
    public class SignalRHubConnectionService : ISignalRHubConnectionService
    {
        private const string HubConnectionAzure = "http://parser-mvc.azurewebsites.net/";
        private const string HubConnectionUrl = "http://localhost:50800";

        private readonly IHubConnectionProvider hubConnectionProvider;
        private readonly IHubProxyProviderFactory hubProxyProviderFactory;

        private readonly IDictionary<string, IHubProxyProvider> hubProxyProviders;

        public SignalRHubConnectionService(IHubConnectionProviderFactory hubConnectionProviderFactory, IHubProxyProviderFactory hubProxyProviderFactory)
        {
            Guard.WhenArgument(hubConnectionProviderFactory, nameof(IHubConnectionProviderFactory)).IsNull().Throw();
            Guard.WhenArgument(hubProxyProviderFactory, nameof(IHubProxyProviderFactory)).IsNull().Throw();

            this.hubConnectionProvider = hubConnectionProviderFactory.CreateHubConnectionProvider(SignalRHubConnectionService.HubConnectionAzure);
            this.hubProxyProviderFactory = hubProxyProviderFactory;

            this.hubProxyProviders = new Dictionary<string, IHubProxyProvider>();

            this.StartHubConnection(this.hubConnectionProvider);
        }

        /// <summary>
        /// Created for testing.
        /// </summary>
        protected IDictionary<string, IHubProxyProvider> HubProxyProviders { get { return this.hubProxyProviders; } }

        public IHubProxyProvider GetHubProxyProvider(string hubName)
        {
            Guard.WhenArgument(hubName, nameof(hubName)).IsNullOrEmpty().Throw();

            if (this.hubProxyProviders.ContainsKey(hubName) == false)
            {
                this.StopHubConnection(this.hubConnectionProvider);

                var newHubProxy = this.hubConnectionProvider.CreateHubProxy(hubName);
                var newHubProxyProvider = this.hubProxyProviderFactory.CreateHubProxyProvider(newHubProxy);

                this.hubProxyProviders.Add(hubName, newHubProxyProvider);

                this.StartHubConnection(this.hubConnectionProvider);
            }

            return this.hubProxyProviders[hubName];
        }

        private void StartHubConnection(IHubConnectionProvider hubConnectionProvider)
        {
            hubConnectionProvider.Start().Wait();
        }

        private void StopHubConnection(IHubConnectionProvider hubConnectionProvider)
        {
            hubConnectionProvider.Stop();
        }
    }
}
