using Parser.Common.Contracts;
using Parser.LogFile.Parser.CommandResolutionHandlers;

namespace Parser.LogFile.Parser.Tests.Mocks
{
    internal class MockDamageCommandResolutionHandler : DamageCommandResolutionHandler
    {
        internal new string ExposedMatchingEventName { get { return base.ExposedMatchingEventName; } }

        internal new ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            return base.HandleCommand(command, combatStatisticsContainer);
        }
    }
}
