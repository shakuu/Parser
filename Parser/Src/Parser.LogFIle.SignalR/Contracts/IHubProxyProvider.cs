using System;
using System.Threading.Tasks;

namespace Parser.LogFile.SignalR.Contracts
{
    public interface IHubProxyProvider
    {
        Task Invoke(string method, params object[] args);

        IDisposable On<T>(string eventName, Action<T> onData);

        IDisposable On<T1, T2>(string eventName, Action<T1, T2> onData);
    }
}
