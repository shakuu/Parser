using System;
using System.Threading;

using Bytes2you.Validation;

using Parser.Auth.Remote;
using Parser.Common.Contracts;
using Parser.LogFile.Reader.Contracts;
using Parser.LogFile.SignalR.Contracts;

namespace Parser.LogFile.SignalR.Strategies
{
    public class SignalRCommandUtilizationStrategy : ICommandUtilizationStrategy, IDisposable
    {
        private const string HubName = "LogFileParserHub";

        private readonly ICommandJsonConvertProvider commandJsonConvertProvider;
        private readonly IRemoteUserProvider remoteUserProvider;

        private readonly IHubProxyProvider logFileParserHubProxyProvider;

        private string parsingSessionId;

        public SignalRCommandUtilizationStrategy(ISignalRHubConnectionService signalRHubConnectionService, ICommandJsonConvertProvider commandJsonConvertProvider, IRemoteUserProvider remoteUserProvider)
        {
            Guard.WhenArgument(signalRHubConnectionService, nameof(ISignalRHubConnectionService)).IsNull().Throw();
            Guard.WhenArgument(commandJsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();
            Guard.WhenArgument(remoteUserProvider, nameof(IRemoteUserProvider)).IsNull().Throw();

            this.commandJsonConvertProvider = commandJsonConvertProvider;
            this.remoteUserProvider = remoteUserProvider;

            this.logFileParserHubProxyProvider = signalRHubConnectionService.GetHubProxyProvider(SignalRCommandUtilizationStrategy.HubName);

            this.InitializeLogFileParserHubProxy(this.logFileParserHubProxyProvider);

            var loggedRemoteUserUsername = this.GetLoggedRemoteUserUsername();
            this.GetParsingSessionid(loggedRemoteUserUsername, this.logFileParserHubProxyProvider);
        }

        /// <summary>
        /// Created for testing.
        /// </summary>
        protected string ParsingSessionId { get { return this.parsingSessionId; } set { this.parsingSessionId = value; } }

        public void UtilizeCommand(ICommand command)
        {
            Guard.WhenArgument(command, nameof(ICommand)).IsNull().Throw();

            var loggedRemoteUserUsername = this.GetLoggedRemoteUserUsername();
            while (string.IsNullOrEmpty(this.parsingSessionId))
            {
                this.GetParsingSessionid(loggedRemoteUserUsername, this.logFileParserHubProxyProvider);
            }

            var serializedCommand = this.commandJsonConvertProvider.SerializeCommand(command);

            this.logFileParserHubProxyProvider.Invoke("SendCommand", this.parsingSessionId, serializedCommand);
        }

        private string GetLoggedRemoteUserUsername()
        {
            return this.remoteUserProvider.LoggedInRemoteUser?.Username;
        }

        private void InitializeLogFileParserHubProxy(IHubProxyProvider logFileParserHubProxyProvider)
        {
            // TODO: DELETE CW
            logFileParserHubProxyProvider.On<string>("UpdateStatus", (update) => System.Console.WriteLine(update));
            logFileParserHubProxyProvider.On<string>("UpdateParsingSessionId", this.OnUpdateParsingSessionId);
        }

        private void GetParsingSessionid(string username, IHubProxyProvider logFileParserHubProxyProvider)
        {
            logFileParserHubProxyProvider.Invoke("GetParsingSessionId", username).Wait();
            Thread.Sleep(500);
        }

        private void OnUpdateParsingSessionId(string parsingSessionId)
        {
            this.parsingSessionId = parsingSessionId;
        }

        public void Dispose()
        {
            this.logFileParserHubProxyProvider.Invoke("ReleaseParsingSessionId", this.parsingSessionId).Wait();
        }
    }
}
