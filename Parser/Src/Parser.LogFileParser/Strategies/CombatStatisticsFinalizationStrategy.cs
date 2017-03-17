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
            Guard.WhenArgument(combatStatistics, nameof(ICombatStatistics)).IsNull().Throw();

            var finalizedCombatStatistics = this.finalizedCombatStatisticsFactory.CreateFinalizedCombatStatistics();

            finalizedCombatStatistics.CharacterName = combatStatistics.CharacterName;
            finalizedCombatStatistics.CombatDurationInSeconds = this.GetCombatDurationInSeconds(combatStatistics);

            finalizedCombatStatistics = this.GetDamageDoneAndDamageDonePerSecond(combatStatistics, finalizedCombatStatistics);
            finalizedCombatStatistics = this.GetDamageTakenAndDamageTakenPerSecond(combatStatistics, finalizedCombatStatistics);

            return finalizedCombatStatistics;
        }

        private double GetCombatDurationInSeconds(ICombatStatistics combatStatistics)
        {
            var timeSpan = combatStatistics.ExitCombatTime - combatStatistics.EnterCombatTime;

            return timeSpan.TotalSeconds;
        }

        private IFinalizedCombatStatistics GetDamageDoneAndDamageDonePerSecond(ICombatStatistics combatStatistics, IFinalizedCombatStatistics finalizedCombatStatistics)
        {
            var damageDone = combatStatistics.DamageDone;
            var damageDonePerSecond = damageDone / finalizedCombatStatistics.CombatDurationInSeconds;

            finalizedCombatStatistics.DamageDone = damageDone;
            finalizedCombatStatistics.DamageDonePerSecond = damageDonePerSecond;

            return finalizedCombatStatistics;
        }

        private IFinalizedCombatStatistics GetDamageTakenAndDamageTakenPerSecond(ICombatStatistics combatStatistics, IFinalizedCombatStatistics finalizedCombatStatistics)
        {
            var damageTaken = combatStatistics.DamageTaken;
            var damageTakenPerSecond = damageTaken / finalizedCombatStatistics.CombatDurationInSeconds;

            finalizedCombatStatistics.DamageTaken = damageTaken;
            finalizedCombatStatistics.DamageTakenPerSecond = damageTakenPerSecond;

            return finalizedCombatStatistics;
        }

        private IFinalizedCombatStatistics GetHealingDoneAndHealingDonePerSecond(ICombatStatistics combatStatistics, IFinalizedCombatStatistics finalizedCombatStatistics)
        {
            var healingDone = combatStatistics.HealingDone;
            var healingDonePerSecond = healingDone / finalizedCombatStatistics.CombatDurationInSeconds;

            finalizedCombatStatistics.DamageTaken = healingDone;
            finalizedCombatStatistics.DamageTakenPerSecond = healingDonePerSecond;

            return finalizedCombatStatistics;
        }
    }
}
