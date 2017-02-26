using Parser.SignalR.Contracts;

namespace Parser.SignalR.Factories
{
    public interface IHubConnectionProviderFactory
    {
        IHubConnectionProvider CreateHubConnectionProvider(string url);
    }
}
