using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Client;

using Bytes2you.Validation;

using Parser.LogFileReader.Contracts;
using Parser.SignalRUtilizationStrategy.Contracts;

namespace Parser.SignalRUtilizationStrategy
{
    public class SignalRCommandUtilizationStrategy : ICommandUtilizationStrategy
    {
        private const string HubConnectionUrl = "http://localhost:52589";

        private readonly IHubConnectionProviderFactory hubConnectionProviderFactory;
        private readonly IJsonConvertProvider jsonConvertProvider;

        private readonly IHubProxy logFileParserHubProxy;
        private string parsingSessionId;

        public SignalRCommandUtilizationStrategy(IHubConnectionProviderFactory hubConnectionProviderFactory, IJsonConvertProvider jsonConvertProvider)
        {
            Guard.WhenArgument(hubConnectionProviderFactory, nameof(IHubConnectionProviderFactory)).IsNull().Throw();
            Guard.WhenArgument(jsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();

            this.hubConnectionProviderFactory = hubConnectionProviderFactory;
            this.jsonConvertProvider = jsonConvertProvider;

            var initializeLogFileParserHubProxyTask = this.InitializeLogFileParserHubProxy(SignalRCommandUtilizationStrategy.HubConnectionUrl);
            this.logFileParserHubProxy = initializeLogFileParserHubProxyTask.Result;
        }

        public void UtilizeCommand(ICommand command)
        {
            var serializedCommand = this.jsonConvertProvider.SerializeObject(command);

            this.logFileParserHubProxy.Invoke("SendCommand", this.parsingSessionId, serializedCommand);
        }

        private async Task<IHubProxy> InitializeLogFileParserHubProxy(string url)
        {
            var connection = this.hubConnectionProviderFactory.CreateHubConnectionProvider(url);

            var logFileParserHubProxy = connection.CreateHubProxy("LogFileParserHub");
            logFileParserHubProxy.On<string>("UpdateParsingSessionId", this.OnUpdateParsingSessionId);

            await connection.Start();
            await logFileParserHubProxy.Invoke("GetParsingSessionId");

            return logFileParserHubProxy;
        }

        private void OnUpdateParsingSessionId(string parsingSessionId)
        {
            this.parsingSessionId = parsingSessionId;
        }
    }
}
