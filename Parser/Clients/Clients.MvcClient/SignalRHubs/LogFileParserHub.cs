using Microsoft.AspNet.SignalR;

using Ninject;

using Clients.MvcClient.App_Start;
using Parser.SignalR.Contracts;

namespace Clients.MvcClient.SignalRHubs
{
    public class LogFileParserHub : Hub
    {
        private readonly ILogFileParserHubService logFileParserHubService;

        public LogFileParserHub()
        {
            this.logFileParserHubService = NinjectWebCommon.Kernel.Get<ILogFileParserHubService>();
        }

        public void SendCommand(string userId, string serializedCommand)
        {
            var message = this.logFileParserHubService.SendCommand(userId, serializedCommand);

            Clients.Caller.ReceiveStatus(message);
        }

        public void GetParsingSessionId()
        {
            var message = this.logFileParserHubService.GetParsingSessionId();

            Clients.Caller.UpdateParsingSessionId(message);
        }
    }
}