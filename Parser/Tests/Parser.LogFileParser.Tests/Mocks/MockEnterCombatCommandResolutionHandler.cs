using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFileParser.CommandResolutionHandlers;

namespace Parser.LogFileParser.Tests.Mocks
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
