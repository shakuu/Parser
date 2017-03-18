namespace Parser.LogFile.SignalR.Contracts
{
    public interface ISignalRHubConnectionService
    {
        IHubProxyProvider GetHubProxyProvider(string hubName);
    }
}
