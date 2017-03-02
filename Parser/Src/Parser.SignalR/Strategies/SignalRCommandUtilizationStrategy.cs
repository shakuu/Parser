using System.Threading;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.LogFileReader.Contracts;
using Parser.SignalR.Contracts;

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

        /// <summary>
        /// Created for testing.
        /// </summary>
        protected string ParsingSessionId { get { return this.parsingSessionId; } set { this.parsingSessionId = value; } }

        public void UtilizeCommand(ICommand command)
        {
            Guard.WhenArgument(command, nameof(ICommand)).IsNull().Throw();

            while (string.IsNullOrEmpty(this.parsingSessionId))
            {
                logFileParserHubProxyProvider.Invoke("GetParsingSessionId").Wait();
                Thread.Sleep(500);
            }

            var serializedCommand = this.jsonConvertProvider.SerializeObject(command);

            this.logFileParserHubProxyProvider.Invoke("SendCommand", this.parsingSessionId, serializedCommand);
        }

        private void InitializeLogFileParserHubProxy(IHubProxyProvider logFileParserHubProxyProvider)
        {
            // TODO: DELETE CW
            logFileParserHubProxyProvider.On<string>("UpdateStatus", (update) => System.Console.WriteLine(update));
            logFileParserHubProxyProvider.On<string>("UpdateParsingSessionId", this.OnUpdateParsingSessionId);
            logFileParserHubProxyProvider.Invoke("GetParsingSessionId").Wait();
        }

        private void OnUpdateParsingSessionId(string parsingSessionId)
        {
            this.parsingSessionId = parsingSessionId;
        }
    }
}
