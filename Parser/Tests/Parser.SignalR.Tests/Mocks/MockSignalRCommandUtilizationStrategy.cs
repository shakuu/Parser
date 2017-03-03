using Parser.Common.Contracts;
using Parser.SignalR.Contracts;
using Parser.SignalR.Strategies;

namespace Parser.SignalR.Tests.Mocks
{
    internal class MockSignalRCommandUtilizationStrategy : SignalRCommandUtilizationStrategy
    {
        internal MockSignalRCommandUtilizationStrategy(ISignalRHubConnectionService signalRHubConnectionService, ICommandJsonConvertProvider commandJsonConvertProvider)
            : base(signalRHubConnectionService, commandJsonConvertProvider)
        {
        }

        internal new string ParsingSessionId { get { return base.ParsingSessionId; } set { base.ParsingSessionId = value; } }
    }
}
