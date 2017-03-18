using Parser.Common.Contracts;

namespace Parser.LogFile.Parser.Contracts
{
    public interface ICombatStatisticsFinalizationStrategy
    {
        IFinalizedCombatStatistics FinalizeCombatStatistics(ICombatStatistics combatStatistics);
    }
}
