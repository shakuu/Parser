using Parser.Auth.Remote;
using Parser.Common.Contracts;
using Parser.LogFile.SignalR.Contracts;
using Parser.LogFile.SignalR.Strategies;

namespace Parser.LogFile.SignalR.Tests.Mocks
{
    internal class MockSignalRCommandUtilizationStrategy : SignalRCommandUtilizationStrategy
    {
        internal MockSignalRCommandUtilizationStrategy(ISignalRHubConnectionService signalRHubConnectionService, ICommandJsonConvertProvider commandJsonConvertProvider, IRemoteUserProvider remoteUserProvider)
            : base(signalRHubConnectionService, commandJsonConvertProvider, remoteUserProvider)
        {
        }

        internal new string ParsingSessionId { get { return base.ParsingSessionId; } set { base.ParsingSessionId = value; } }
    }
}
