using System;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.Strategies
{
    public class CombatStatisticsFinalizationStrategy : ICombatStatisticsFinalizationStrategy
    {
        public IFinalizedCombatStatistics FinalizeCombatStatistics(ICombatStatistics combatStatistics)
        {
            throw new NotImplementedException();
        }
    }
}
