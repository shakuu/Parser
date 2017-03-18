using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFile.Parser.CommandResolutionHandlers;

namespace Parser.LogFile.Parser.Tests.Mocks
{
    internal class MockEnterCombatCommandResolutionHandler : EnterCombatCommandResolutionHandler
    {
        internal MockEnterCombatCommandResolutionHandler(ICombatStatisticsFactory combatStatisticsFactory)
            : base(combatStatisticsFactory)
        {
        }

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
