using System;
using Parser.SignalR.Contracts;

namespace Parser.SignalR.Services
{
    public class LogFileParserHubService : ILogFileParserHubService
    {
        public string GetParsingSessionId()
        {
            return Guid.NewGuid().ToString();
        }

        public string SendCommand(string userId, string serializedCommand)
        {
            return "updated";
        }
    }
}
