using Parser.SignalR.Contracts;
using Parser.SignalR.Strategies;

namespace Parser.SignalR.Tests.Mocks
{
    internal class MockSignalRCommandUtilizationStrategy : SignalRCommandUtilizationStrategy
    {
        public MockSignalRCommandUtilizationStrategy(ISignalRHubConnectionService signalRHubConnectionService, IJsonConvertProvider jsonConvertProvider)
            : base(signalRHubConnectionService, jsonConvertProvider)
        {
        }

        public new string ParsingSessionId { get { return base.ParsingSessionId; } set { base.ParsingSessionId = value; } }
    }
}
