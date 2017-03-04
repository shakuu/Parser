using System;
using System.Threading;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.LogFileReader.Contracts;
using Parser.SignalR.Contracts;

namespace Parser.SignalR.Strategies
{
    public class SignalRCommandUtilizationStrategy : ICommandUtilizationStrategy, IDisposable
    {
        private const string HubName = "LogFileParserHub";

        private readonly ICommandJsonConvertProvider commandJsonConvertProvider;

        private readonly IHubProxyProvider logFileParserHubProxyProvider;

        private string parsingSessionId;

        public SignalRCommandUtilizationStrategy(ISignalRHubConnectionService signalRHubConnectionService, ICommandJsonConvertProvider commandJsonConvertProvider)
        {
            Guard.WhenArgument(signalRHubConnectionService, nameof(ISignalRHubConnectionService)).IsNull().Throw();
            Guard.WhenArgument(commandJsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();

            this.commandJsonConvertProvider = commandJsonConvertProvider;

            this.logFileParserHubProxyProvider = signalRHubConnectionService.GetHubProxyProvider(SignalRCommandUtilizationStrategy.HubName);

            this.InitializeLogFileParserHubProxy(this.logFileParserHubProxyProvider);
            this.GetParsingSessionid(this.logFileParserHubProxyProvider);
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
                this.GetParsingSessionid(this.logFileParserHubProxyProvider);
            }

            var serializedCommand = this.commandJsonConvertProvider.SerializeCommand(command);

            this.logFileParserHubProxyProvider.Invoke("SendCommand", this.parsingSessionId, serializedCommand);
        }

        private void InitializeLogFileParserHubProxy(IHubProxyProvider logFileParserHubProxyProvider)
        {
            // TODO: DELETE CW
            logFileParserHubProxyProvider.On<string>("UpdateStatus", (update) => System.Console.WriteLine(update));
            logFileParserHubProxyProvider.On<string>("UpdateParsingSessionId", this.OnUpdateParsingSessionId);
        }

        private void GetParsingSessionid(IHubProxyProvider logFileParserHubProxyProvider)
        {
            logFileParserHubProxyProvider.Invoke("GetParsingSessionId").Wait();
            Thread.Sleep(500);
        }

        private void OnUpdateParsingSessionId(string parsingSessionId)
        {
            this.parsingSessionId = parsingSessionId;
        }

        public void Dispose()
        {
            this.logFileParserHubProxyProvider.Invoke("ReleaseParsingSessionId", this.parsingSessionId);
        }
    }
}
