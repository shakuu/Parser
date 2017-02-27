﻿using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.SignalR.Contracts;
using Parser.SignalR.Factories;

namespace Parser.SignalR.Services
{
    public class SignalRHubConnectionService : ISignalRHubConnectionService
    {
        private const string HubConnectionUrl = "http://localhost:50800";

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

            this.StartHubConnection(this.hubConnectionProvider);
        }

        public IHubProxyProvider GetHubProxyProvider(string hubName)
        {
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