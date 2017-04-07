using System;
using System.Collections.Generic;

using Parser.Auth.Remote;
using Parser.Common.Contracts;
using Parser.LogFile.Reader.Contracts;
using Parser.LogFile.SignalR.Contracts;
using Parser.LogFile.SignalR.Strategies;

namespace Parser.LogFIle.SignalR.Strategies
{
    public class SignalRForceCommandUtilizationStrategy : SignalRCommandUtilizationStrategy, IForceCommandUtilizationStrategy, ICommandUtilizationStrategy, IDisposable
    {
        private const int MaximumCommandsCountAllowed = 29;

        private readonly ICommandEnumerationJsonConvertProvider commandEnumerationJsonConvertProvider;

        private readonly ICollection<ICommand> commands;

        public SignalRForceCommandUtilizationStrategy(ICommandUtilizationUpdateStrategy commandUtilizationUpdateStrategy, ISignalRHubConnectionService signalRHubConnectionService, ICommandEnumerationJsonConvertProvider commandEnumerationJsonConvertProvider, IRemoteUserProvider remoteUserProvider)
            : base(commandUtilizationUpdateStrategy, signalRHubConnectionService, commandEnumerationJsonConvertProvider, remoteUserProvider)
        {
            this.commandEnumerationJsonConvertProvider = commandEnumerationJsonConvertProvider;

            this.commands = new LinkedList<ICommand>();
        }

        public override void UtilizeCommand(ICommand command)
        {
            base.ValidateCommand(command);

            this.commands.Add(command);
            if (this.commands.Count >= SignalRForceCommandUtilizationStrategy.MaximumCommandsCountAllowed)
            {
                this.ForceUtilizeCommand();
            }
        }

        public void ForceUtilizeCommand()
        {
            if (this.commands.Count == 0)
            {
                return;
            }

            base.VerifyGetParsingSessionId(base.LogFileParserHubProxyProvider);

            var serializedCommandEnumeration = this.commandEnumerationJsonConvertProvider.SerializeCommandEnumeration(this.commands);
            base.LogFileParserHubProxyProvider.Invoke("SendCommandEnumeration", base.ParsingSessionId, serializedCommandEnumeration);

            this.commands.Clear();
        }
    }
}
