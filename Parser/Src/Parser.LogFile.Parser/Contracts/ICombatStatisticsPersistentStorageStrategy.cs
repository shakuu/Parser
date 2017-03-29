using Parser.Common.Contracts;

namespace Parser.LogFile.Parser.Contracts
{
    public interface ICombatStatisticsPersistentStorageStrategy
    {
        IFinalizedCombatStatistics StoreCombatStatistics(IFinalizedCombatStatistics combatStatistics);
    }
}
