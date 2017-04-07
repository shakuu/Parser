namespace Parser.LogFile.SignalR.Contracts
{
    public interface ILogFileParserHub
    {
        void SendCommand(string engineId, string serializedCommand);

        void EndParsingSession(string engineId);

        void GetParsingSessionId(string username);

        void SendCommandEnumeration(string engineId, string serializedCommand);
    }
}
