namespace Parser.SignalR.Contracts
{
    public interface ILogFileParseHub
    {
        void SendCommand(string engineId, string serializedCommand);

        void EndParsingSession(string engineId);

        void GetParsingSessionId();
    }
}
