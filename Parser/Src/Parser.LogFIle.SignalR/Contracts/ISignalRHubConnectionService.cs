namespace Parser.SignalR.Contracts
{
    public interface ISignalRHubConnectionService
    {
        IHubProxyProvider GetHubProxyProvider(string hubName);
    }
}
