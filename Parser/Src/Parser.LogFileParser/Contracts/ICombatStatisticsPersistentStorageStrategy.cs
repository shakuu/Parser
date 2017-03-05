using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ICombatStatisticsPersistentStorageStrategy
    {
        ICombatStatistics StoreCombatStatistics(ICombatStatistics combatStatistics);
    }
}
