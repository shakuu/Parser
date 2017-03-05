using Parser.Common.Contracts;
using Parser.LogFileParser.CommandResolutionHandlers;

namespace Parser.LogFileParser.Tests.Mocks
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
