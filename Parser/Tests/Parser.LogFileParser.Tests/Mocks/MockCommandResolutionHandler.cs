using Parser.Common.Contracts;
using Parser.LogFileParser.CommandResolutionHandlers.Base;

namespace Parser.LogFileParser.Tests.Mocks
{
    internal class MockCommandResolutionHandler : CommandResolutionHandler
    {
        internal bool CanHandleCommandMethodIsCalled { get; set; }

        internal bool CanHandleCommandMethodReturnValue { get; set; }

        internal bool HandleCommandMethodIsCalled { get; set; }

        internal ICombatStatisticsContainer HandleCommandMethodReturnValue { get; set; }

        protected override bool CanHandleCommand(ICommand command)
        {
            this.CanHandleCommandMethodIsCalled = true;

            return this.CanHandleCommandMethodReturnValue;
        }

        protected override ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            this.HandleCommandMethodIsCalled = true;

            return this.HandleCommandMethodReturnValue;
        }
    }
}
