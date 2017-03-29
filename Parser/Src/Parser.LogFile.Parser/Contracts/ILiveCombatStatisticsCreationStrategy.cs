using Parser.Common.Contracts;

namespace Parser.LogFile.Parser.Contracts
{
    public interface ILiveCombatStatisticsCreationStrategy
    {
        ILiveCombatStatistics CreateLiveCombatStatistics(ICombatStatistics combatStatistics);
    }
}
