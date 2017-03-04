using Parser.Common.Contracts;
using Parser.LogFileParser.CommandResolutionHandlers.Base;

namespace Parser.LogFileParser.Tests.Mocks
{
    internal class MockCommandResolutionHandler : CommandResolutionHandler
    {
        public MockCommandResolutionHandler()
            : base("MockCommand")
        {

        }

        internal bool CanHandleCommandMethodIsCalled { get; set; }

        internal ICommand CanHandleCommandMethodICommandParameter { get; set; }

        internal bool CanHandleCommandMethodReturnValue { get; set; }

        internal bool HandleCommandMethodIsCalled { get; set; }

        internal ICommand HandleCommandMethodICommandParameter { get; set; }

        internal ICombatStatisticsContainer HandleCommandMethodICombatStatisticsContainerParameter { get; set; }

        internal ICombatStatisticsContainer HandleCommandMethodReturnValue { get; set; }

        protected override bool CanHandleCommand(ICommand command)
        {
            this.CanHandleCommandMethodICommandParameter = command;
            this.CanHandleCommandMethodIsCalled = true;

            return this.CanHandleCommandMethodReturnValue;
        }

        protected override ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            this.HandleCommandMethodICommandParameter = command;
            this.HandleCommandMethodICombatStatisticsContainerParameter = combatStatisticsContainer;

            this.HandleCommandMethodIsCalled = true;

            return this.HandleCommandMethodReturnValue;
        }
    }
}
