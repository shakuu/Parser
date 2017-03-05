using Parser.Common.Contracts;
using Parser.Common.EventsArgs;

namespace Parser.Common.Factories
{
    public interface ICurrentCombatStatisticsChangedEventArgsFactory
    {
        CurrentCombatStatisticsChangedEventArgs CreateCurrentCombatStatisticsChangedEventArgs(ICombatStatistics combatStatistics);
    }
}
