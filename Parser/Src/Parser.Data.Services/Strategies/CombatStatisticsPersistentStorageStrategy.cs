using System;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;

namespace Parser.Data.Services.Strategies
{
    public class CombatStatisticsPersistentStorageStrategy : ICombatStatisticsPersistentStorageStrategy
    {
        public IFinalizedCombatStatistics StoreCombatStatistics(IFinalizedCombatStatistics finalizedCombatStatistics)
        {
            throw new NotImplementedException();
        }
    }
}
