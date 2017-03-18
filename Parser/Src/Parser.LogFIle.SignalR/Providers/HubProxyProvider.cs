using System;
using System.Threading.Tasks;

using Bytes2you.Validation;

using Microsoft.AspNet.SignalR.Client;

using Parser.SignalR.Contracts;

namespace Parser.SignalR.Providers
{
    public class HubProxyProvider : IHubProxyProvider
    {
        private readonly IHubProxy hubProxy;

        public HubProxyProvider(IHubProxy hubProxy)
        {
            Guard.WhenArgument(hubProxy, nameof(IHubProxy)).IsNull().Throw();

            this.hubProxy = hubProxy;
        }

        public Task Invoke(string method, params object[] args)
        {
            return this.hubProxy.Invoke(method, args);
        }

        public IDisposable On<T>(string eventName, Action<T> onData)
        {
            return this.hubProxy.On<T>(eventName, onData);
        }

        public IDisposable On<T1, T2>(string eventName, Action<T1, T2> onData)
        {
            return this.hubProxy.On<T1, T2>(eventName, onData);
        }
    }
}
