using Parser.LogFile.SignalR.Contracts;

namespace Parser.LogFile.SignalR.Factories
{
    public interface IHubConnectionProviderFactory
    {
        IHubConnectionProvider CreateHubConnectionProvider(string url);
    }
}
