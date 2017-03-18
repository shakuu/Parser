using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ICombatStatisticsPersistentStorageStrategy
    {
        IFinalizedCombatStatistics StoreCombatStatistics(IFinalizedCombatStatistics combatStatistics);
    }
}
