using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ICombatStatisticsFinalizationStrategy
    {
        IFinalizedCombatStatistics FinalizeCombatStatistics(ICombatStatistics combatStatistics);
    }
}
