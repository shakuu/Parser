using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.LogFileReader.Contracts;
using Parser.SignalR.Contracts;

namespace Parser.CommandUtilizationStrategies
{
    public class SignalRCommandUtilizationStrategy : ICommandUtilizationStrategy
    {
        private const string HubConnectionUrl = "http://localhost:52589";

        private readonly IJsonConvertProvider jsonConvertProvider;

        private readonly IHubProxyProvider logFileParserHubProxy;

        private string parsingSessionId;

        public SignalRCommandUtilizationStrategy(ISignalRHubConnectionService signalRHubConnectionService, IJsonConvertProvider jsonConvertProvider)
        {
            Guard.WhenArgument(signalRHubConnectionService, nameof(ISignalRHubConnectionService)).IsNull().Throw();
            Guard.WhenArgument(jsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();

            this.jsonConvertProvider = jsonConvertProvider;

            this.logFileParserHubProxy = signalRHubConnectionService.GetHubProxyProvider("LogFileParserHub");
            this.InitializeLogFileParserHubProxy(this.logFileParserHubProxy);
        }

        public void UtilizeCommand(ICommand command)
        {
            Guard.WhenArgument(command, nameof(ICommand)).IsNull().Throw();

            var serializedCommand = this.jsonConvertProvider.SerializeObject(command);

            this.logFileParserHubProxy.Invoke("SendCommand", this.parsingSessionId, serializedCommand);
        }

        private async void InitializeLogFileParserHubProxy(IHubProxyProvider logFileParserHubProxy)
        {
            logFileParserHubProxy.On<string>("UpdateParsingSessionId", this.OnUpdateParsingSessionId);
            await logFileParserHubProxy.Invoke("GetParsingSessionId");
        }

        private void OnUpdateParsingSessionId(string parsingSessionId)
        {
            this.parsingSessionId = parsingSessionId;
        }
    }
}
