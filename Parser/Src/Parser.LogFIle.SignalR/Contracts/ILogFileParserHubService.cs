namespace Parser.LogFile.SignalR.Contracts
{
    public interface ILogFileParserHubService
    {
        string SendCommand(string engineId, string serializedCommand);

        string ReleaseParsingSessionId(string engineId);

        string GetParsingSessionId(string username);

        string SendCommandEnumeration(string engineId, string serializedCommandEnumeration);
    }
}
