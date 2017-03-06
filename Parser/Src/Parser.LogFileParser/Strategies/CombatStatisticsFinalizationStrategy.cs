using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.Strategies
{
    public class CombatStatisticsFinalizationStrategy : ICombatStatisticsFinalizationStrategy
    {
        private readonly IFinalizedCombatStatisticsFactory finalizedCombatStatisticsFactory;

        public CombatStatisticsFinalizationStrategy(IFinalizedCombatStatisticsFactory finalizedCombatStatisticsFactory)
        {
            Guard.WhenArgument(finalizedCombatStatisticsFactory, nameof(IFinalizedCombatStatisticsFactory)).IsNull().Throw();

            this.finalizedCombatStatisticsFactory = finalizedCombatStatisticsFactory;
        }

        public IFinalizedCombatStatistics FinalizeCombatStatistics(ICombatStatistics combatStatistics)
        {
            var finalizedCombatStatistics = this.finalizedCombatStatisticsFactory.CreateFinalizedCombatStatistics();

            return finalizedCombatStatistics;
        }

        private double GetCombatDuration(ICombatStatistics combatStatistics)
        {
            var timeSpan = combatStatistics.ExitCombatTime - combatStatistics.EnterCombatTime;

            return timeSpan.TotalSeconds;
        }
    }
}
