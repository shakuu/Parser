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

        public LiveCombatStatisticsCreationStrategy(ILiveCombatStatisticsFactory liveCombatStatisticsFactory)
        {
            Guard.WhenArgument(liveCombatStatisticsFactory, nameof(ILiveCombatStatisticsFactory)).IsNull().Throw();

            this.liveCombatStatisticsFactory = liveCombatStatisticsFactory;
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
            if (combatStatistics.EnterCombatTime > combatStatistics.ExitCombatTime)
            {
                combatStatistics.ExitCombatTime = combatStatistics.ExitCombatTime.AddDays(1);
            }

            return combatStatistics.ExitCombatTime - combatStatistics.EnterCombatTime;
        }

        private double GetDamageDonePerSecond(ILiveCombatStatistics liveCombatStatistics, ICombatStatistics combatStatistics)
        {
            return combatStatistics.DamageDone / liveCombatStatistics.CombatDuration.TotalSeconds;
        }

        private double GetHealingDonePerSecond(ILiveCombatStatistics liveCombatStatistics, ICombatStatistics combatStatistics)
        {
            return combatStatistics.HealingDone / liveCombatStatistics.CombatDuration.TotalSeconds;
        }
    }
}
