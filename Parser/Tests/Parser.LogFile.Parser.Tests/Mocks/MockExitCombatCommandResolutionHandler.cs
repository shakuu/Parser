using Parser.Common.Contracts;
using Parser.LogFileParser.CommandResolutionHandlers;

namespace Parser.LogFileParser.Tests.Mocks
{
    internal class MockExitCombatCommandResolutionHandler : ExitCombatCommandResolutionHandler
    {
        internal new bool CanHandleCommand(ICommand command)
        {
            return base.CanHandleCommand(command);
        }

        internal new ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            return base.HandleCommand(command, combatStatisticsContainer);
        }
    }
}
