namespace Parser.LogFileReader.Contracts
{
    public interface ILogFilePathDiscoveryStrategy
    {
        string DiscoverLogFile();
    }
}
