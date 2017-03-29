using Parser.Common.Contracts;
using Parser.LogFile.Parser.CommandResolutionHandlers;

namespace Parser.LogFile.Parser.Tests.Mocks
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
