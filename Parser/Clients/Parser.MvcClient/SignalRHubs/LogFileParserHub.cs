using Microsoft.AspNet.SignalR;

using Ninject;

using Parser.MvcClient.App_Start;
using Parser.SignalR.Contracts;

namespace Parser.MvcClient.SignalRHubs
{
    public class LogFileParserHub : Hub
    {
        private readonly ILogFileParserHubService logFileParserHubService;

        public LogFileParserHub()
        {
            this.logFileParserHubService = NinjectWebCommon.Kernel.Get<ILogFileParserHubService>();
        }

        public void SendCommand(string emgineId, string serializedCommand)
        {
            var message = this.logFileParserHubService.SendCommand(emgineId, serializedCommand);

            Clients.Caller.UpdateStatus(message);
        }

        public void GetParsingSessionId()
        {
            var message = this.logFileParserHubService.GetParsingSessionId();

            Clients.Caller.UpdateParsingSessionId(message);
        }
    }
}