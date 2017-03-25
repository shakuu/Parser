using Parser.Auth.Remote;
using Parser.Common.Contracts;
using Parser.LogFile.Reader.Contracts;
using Parser.LogFile.SignalR.Contracts;
using Parser.LogFile.SignalR.Strategies;

namespace Parser.LogFile.SignalR.Tests.Mocks
{
    internal class MockSignalRCommandUtilizationStrategy : SignalRCommandUtilizationStrategy
    {
        internal MockSignalRCommandUtilizationStrategy(ICommandUtilizationUpdateStrategy commandUtilizationUpdateStrategy, ISignalRHubConnectionService signalRHubConnectionService, ICommandJsonConvertProvider commandJsonConvertProvider, IRemoteUserProvider remoteUserProvider)
            : base(commandUtilizationUpdateStrategy, signalRHubConnectionService, commandJsonConvertProvider, remoteUserProvider)
        {
        }

        internal new string ParsingSessionId { get { return base.ParsingSessionId; } set { base.ParsingSessionId = value; } }
    }
}
