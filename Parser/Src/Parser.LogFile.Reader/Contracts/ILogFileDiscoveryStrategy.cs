namespace Parser.LogFile.Reader.Contracts
{
    public interface ILogFilePathDiscoveryStrategy
    {
        string DiscoverLogFile();
    }
}
