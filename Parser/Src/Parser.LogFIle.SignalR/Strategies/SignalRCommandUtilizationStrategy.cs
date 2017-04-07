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

        private readonly ICommandUtilizationUpdateStrategy commandUtilizationUpdateStrategy;
        private readonly ICommandJsonConvertProvider commandJsonConvertProvider;
        private readonly IRemoteUserProvider remoteUserProvider;

        private readonly IHubProxyProvider logFileParserHubProxyProvider;

        private string parsingSessionId;

        public SignalRCommandUtilizationStrategy(ICommandUtilizationUpdateStrategy commandUtilizationUpdateStrategy, ISignalRHubConnectionService signalRHubConnectionService, ICommandJsonConvertProvider commandJsonConvertProvider, IRemoteUserProvider remoteUserProvider)
        {
            Guard.WhenArgument(signalRHubConnectionService, nameof(ISignalRHubConnectionService)).IsNull().Throw();

            Guard.WhenArgument(commandUtilizationUpdateStrategy, nameof(ICommandUtilizationUpdateStrategy)).IsNull().Throw();
            Guard.WhenArgument(commandJsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();
            Guard.WhenArgument(remoteUserProvider, nameof(IRemoteUserProvider)).IsNull().Throw();

            this.commandUtilizationUpdateStrategy = commandUtilizationUpdateStrategy;
            this.commandJsonConvertProvider = commandJsonConvertProvider;
            this.remoteUserProvider = remoteUserProvider;

            this.logFileParserHubProxyProvider = signalRHubConnectionService.GetHubProxyProvider(SignalRCommandUtilizationStrategy.HubName);

            this.InitializeLogFileParserHubProxy(this.logFileParserHubProxyProvider);

            this.GetParsingSessionId(this.logFileParserHubProxyProvider);
        }

        protected IHubProxyProvider LogFileParserHubProxyProvider { get { return this.logFileParserHubProxyProvider; } }

        protected string ParsingSessionId { get { return this.parsingSessionId; } set { this.parsingSessionId = value; } }

        public virtual void UtilizeCommand(ICommand command)
        {
            this.ValidateCommand(command);

            this.VerifyGetParsingSessionId(this.logFileParserHubProxyProvider);

            var serializedCommand = this.commandJsonConvertProvider.SerializeCommand(command);

            this.logFileParserHubProxyProvider.Invoke("SendCommand", this.parsingSessionId, serializedCommand);
        }

        protected void VerifyGetParsingSessionId(IHubProxyProvider logFileParserHubProxyProvider)
        {
            while (string.IsNullOrEmpty(this.parsingSessionId))
            {
                this.GetParsingSessionId(this.logFileParserHubProxyProvider);
            }
        }

        protected void ValidateCommand(ICommand command)
        {
            Guard.WhenArgument(command, nameof(ICommand)).IsNull().Throw();
        }

        private string GetLoggedRemoteUserUsername()
        {
            return this.remoteUserProvider.GetLoggedInRemoteUser()?.Username;
        }

        private void InitializeLogFileParserHubProxy(IHubProxyProvider logFileParserHubProxyProvider)
        {
            logFileParserHubProxyProvider.On<string>("UpdateStatus", (update) => this.commandUtilizationUpdateStrategy.DisplayUpdate(update));
            logFileParserHubProxyProvider.On<string>("UpdateParsingSessionId", this.OnUpdateParsingSessionId);
        }

        private void GetParsingSessionId(IHubProxyProvider logFileParserHubProxyProvider)
        {
            var loggedRemoteUserUsername = this.GetLoggedRemoteUserUsername();
            logFileParserHubProxyProvider.Invoke("GetParsingSessionId", loggedRemoteUserUsername).Wait();
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
