using System;

using Microsoft.AspNet.SignalR;

using Ninject;

using Clients.MvcClient.App_Start;

using Parser.Common.Contracts;
using Parser.LogFileReader.Models;

namespace Clients.MvcClient.SignalRHubs
{
    public class LogFileParserHub : Hub
    {
        private readonly IJsonConvertProvider jsonConvertProvider;

        public LogFileParserHub()
        {
            this.jsonConvertProvider = NinjectWebCommon.Kernel.Get<IJsonConvertProvider>();
        }

        public void SendCommand(string userId, string serializedCommand)
        {
            var command = this.jsonConvertProvider.DeserializeObject<Command>(serializedCommand);

            Clients.Caller.ReceiveStatus(command.TimeStamp.ToShortTimeString());
        }

        public void GetParsingSessionId()
        {
            Clients.Caller.UpdateParsingSessionId(Guid.NewGuid());
        }
    }
}