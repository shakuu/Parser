using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ICombatStatisticsFinalizationStrategy
    {
        ICombatStatistics FinalizeCombatStatistics(ICombatStatistics combatStatistics);
    }
}
