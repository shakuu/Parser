using Bytes2you.Validation;

using Parser.SignalR.Contracts;
using Parser.LogFileReader.Contracts;

namespace Parser.SignalR.Strategies
{
    public class SignalRCommandUtilizationStrategy : ICommandUtilizationStrategy
    {
        private const string HubName = "LogFileParserHub";

        private readonly IJsonConvertProvider jsonConvertProvider;

        private readonly IHubProxyProvider logFileParserHubProxyProvider;

        private string parsingSessionId;

        public SignalRCommandUtilizationStrategy(ISignalRHubConnectionService signalRHubConnectionService, IJsonConvertProvider jsonConvertProvider)
        {
            Guard.WhenArgument(signalRHubConnectionService, nameof(ISignalRHubConnectionService)).IsNull().Throw();
            Guard.WhenArgument(jsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();

            this.jsonConvertProvider = jsonConvertProvider;

            this.logFileParserHubProxyProvider = signalRHubConnectionService.GetHubProxyProvider(SignalRCommandUtilizationStrategy.HubName);
            this.InitializeLogFileParserHubProxy(this.logFileParserHubProxyProvider);
        }

        public void UtilizeCommand(ICommand command)
        {
            Guard.WhenArgument(command, nameof(ICommand)).IsNull().Throw();

            var serializedCommand = this.jsonConvertProvider.SerializeObject(command);

            this.logFileParserHubProxyProvider.Invoke("SendCommand", this.parsingSessionId, serializedCommand);
        }

        private void InitializeLogFileParserHubProxy(IHubProxyProvider logFileParserHubProxyProvider)
        {
            // TODO: DELETE CW
            logFileParserHubProxyProvider.On<string>("ReceiveStatus", (update) => System.Console.WriteLine(update));
            logFileParserHubProxyProvider.On<string>("UpdateParsingSessionId", this.OnUpdateParsingSessionId);
            logFileParserHubProxyProvider.Invoke("GetParsingSessionId").Wait();
        }

        private void OnUpdateParsingSessionId(string parsingSessionId)
        {
            this.parsingSessionId = parsingSessionId;
        }
    }
}
