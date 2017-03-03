namespace Parser.SignalR.Contracts
{
    public interface ILogFileParserHubService
    {
        string SendCommand(string engineId, string serializedCommand);

        string GetParsingSessionId();
    }
}
