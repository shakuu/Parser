using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace Parser.SignalRUtilizationStrategy.Contracts
{
    public interface IHubConnectionProvider
    {
        Task Start();

        IHubProxy CreateHubProxy(string hubName);
    }
}
