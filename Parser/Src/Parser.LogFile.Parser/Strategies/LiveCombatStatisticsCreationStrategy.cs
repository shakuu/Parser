using System;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFile.Parser.Contracts;

namespace Parser.LogFile.Parser.Strategies
{
    public class LiveCombatStatisticsCreationStrategy : ILiveCombatStatisticsCreationStrategy
    {
        private readonly ILiveCombatStatisticsFactory liveCombatStatisticsFactory;
        private readonly IDateTimeProvider dateTimeProvider;

        public LiveCombatStatisticsCreationStrategy(ILiveCombatStatisticsFactory liveCombatStatisticsFactory, IDateTimeProvider dateTimeProvider)
        {
            Guard.WhenArgument(liveCombatStatisticsFactory, nameof(ILiveCombatStatisticsFactory)).IsNull().Throw();
            Guard.WhenArgument(dateTimeProvider, nameof(IDateTimeProvider)).IsNull().Throw();

            this.liveCombatStatisticsFactory = liveCombatStatisticsFactory;
            this.dateTimeProvider = dateTimeProvider;
        }

        public ILiveCombatStatistics CreateLiveCombatStatistics(ICombatStatistics combatStatistics)
        {
            if (combatStatistics == null)
            {
                return null;
            }

            var liveCombatStatistics = this.liveCombatStatisticsFactory.CreateLiveCombatStatistics();

            liveCombatStatistics.CharacterName = combatStatistics.CharacterName;
            liveCombatStatistics.CombatDuration = this.GetCombatDuration(combatStatistics);
            liveCombatStatistics.DamageDonePerSecond = this.GetDamageDonePerSecond(liveCombatStatistics, combatStatistics);
            liveCombatStatistics.HealingDonePerSecond = this.GetHealingDonePerSecond(liveCombatStatistics, combatStatistics);

            return liveCombatStatistics;
        }

        private TimeSpan GetCombatDuration(ICombatStatistics combatStatistics)
        {
            return combatStatistics.ExitCombatTime - combatStatistics.EnterCombatTime;
        }

        private double GetDamageDonePerSecond(ILiveCombatStatistics liveCombatStatistics, ICombatStatistics combatStatistics)
        {
            return combatStatistics.DamageDone / liveCombatStatistics.CombatDuration.Seconds;
        }

        private double GetHealingDonePerSecond(ILiveCombatStatistics liveCombatStatistics, ICombatStatistics combatStatistics)
        {
            return combatStatistics.HealingDone / liveCombatStatistics.CombatDuration.Seconds;
        }
    }
}
