using Parser.Common.Contracts;
using Parser.LogFileParser.CommandResolutionHandlers.Base;

namespace Parser.LogFileParser.Tests.Mocks
{
    internal class MockCommandResolutionHandler : CommandResolutionHandler
    {
        public bool CanHandleCommandMethodIsCalled { get; set; }

        internal bool HandleCommandMethodIsCalled { get; set; }

        protected override bool CanHandleCommand(ICommand command)
        {
            this.CanHandleCommandMethodIsCalled = true;

            return true;
        }

        protected override ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            this.HandleCommandMethodIsCalled = true;

            return null;
        }
    }
}
