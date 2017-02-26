namespace Parser.SignalRUtilizationStrategy.Contracts
{
    public interface IHubConnectionProviderFactory
    {
        IHubConnectionProvider CreateHubConnectionProvider(string url);
    }
}
