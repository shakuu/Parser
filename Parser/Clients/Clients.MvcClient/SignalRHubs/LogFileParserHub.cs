using Microsoft.AspNet.SignalR;

using Newtonsoft.Json;

using Parser.LogFileReader.Models;

namespace Clients.MvcClient.SignalRHubs
{
    public class LogFileParserHub : Hub
    {
        public void SendCommand(string serializedCommand)
        {
            var command = JsonConvert.DeserializeObject<Command>(serializedCommand);

            Clients.Caller.ReceiveStatus(command.TimeStamp.ToShortTimeString());
        }
    }
}