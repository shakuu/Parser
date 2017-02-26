using System;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Client;

using Newtonsoft.Json;

using Parser.LogFileReader.Contracts;

namespace Parser.LogFileReader.Strategies
{
    public class SignalRCommandUtilizationStrategy : ICommandUtilizationStrategy
    {
        private readonly IHubProxy logFileParserHubProxy;

        public SignalRCommandUtilizationStrategy()
        {
            this.logFileParserHubProxy = this.CreateProxy("http://localhost:52589").Result;
        }

        public void UtilizeCommand(ICommand command)
        {
            var serializedCommand = JsonConvert.SerializeObject(command);

            this.logFileParserHubProxy.Invoke("SendCommand", serializedCommand);
        }

        private async Task<IHubProxy> CreateProxy(string url)
        {
            var connection = new HubConnection(url);

            var logFileParserHubProxy = connection.CreateHubProxy("LogFileParserHub");

            logFileParserHubProxy.On<string>("ReceiveStatus", (status) =>
            {
                Console.WriteLine(status);
            });

            await connection.Start();

            return logFileParserHubProxy;
        }
    }
}
