using Microsoft.AspNet.SignalR;

using Bytes2you.Validation;

using Parser.LogFile.SignalR.Contracts;

namespace Parser.MvcClient.SignalRHubs
{
    public class LogFileParserHub : Hub, ILogFileParserHub
    {
        private readonly ILogFileParserHubService logFileParserHubService;
        
        public LogFileParserHub(ILogFileParserHubService logFileParserHubService)
        {
            Guard.WhenArgument(logFileParserHubService, nameof(ILogFileParserHubService)).IsNull().Throw();

            this.logFileParserHubService = logFileParserHubService;
        }

        public void SendCommand(string engineId, string serializedCommand)
        {
            var message = this.logFileParserHubService.SendCommand(engineId, serializedCommand);

            Clients.Caller.UpdateStatus(message);
        }

        public void EndParsingSession(string engineId)
        {
            this.logFileParserHubService.ReleaseParsingSessionId(engineId);
        }

        public void GetParsingSessionId(string username)
        {
            var message = this.logFileParserHubService.GetParsingSessionId(username);

            Clients.Caller.UpdateParsingSessionId(message);
        }

        public void SendCommandEnumeration(string engineId, string serializedCommandEnumeration)
        {
            var message = this.logFileParserHubService.SendCommandEnumeration(engineId, serializedCommandEnumeration);

            Clients.Caller.UpdateStatus(message);
        }
    }
}