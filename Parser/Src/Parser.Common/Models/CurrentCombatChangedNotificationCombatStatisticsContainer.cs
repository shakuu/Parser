using System;
using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.EventsArgs;
using Parser.Common.Factories;

namespace Parser.Common.Models
{
    public class CurrentCombatChangedNotificationCombatStatisticsContainer : ICurrentCombatChangedNotificationCombatStatisticsContainer
    {
        public event EventHandler<CurrentCombatStatisticsChangedEventArgs> OnCurrentCombatStatisticsChanged;

        private readonly ICombatStatisticsContainer combatStatisticsContainer;
        private readonly ICurrentCombatStatisticsChangedEventArgsFactory currentCombatStatisticsChangedEventArgsFactory;

        public CurrentCombatChangedNotificationCombatStatisticsContainer(ICombatStatisticsContainer combatStatisticsContainer, ICurrentCombatStatisticsChangedEventArgsFactory currentCombatStatisticsChangedEventArgsFactory)
        {
            Guard.WhenArgument(combatStatisticsContainer, nameof(ICombatStatisticsContainer)).IsNull().Throw();
            Guard.WhenArgument(currentCombatStatisticsChangedEventArgsFactory, nameof(ICurrentCombatStatisticsChangedEventArgsFactory)).IsNull().Throw();

            this.combatStatisticsContainer = combatStatisticsContainer;
            this.currentCombatStatisticsChangedEventArgsFactory = currentCombatStatisticsChangedEventArgsFactory;
        }

        public ICollection<ICombatStatistics> AllCombatStatistics
        {
            get
            {
                return this.combatStatisticsContainer.AllCombatStatistics;
            }

            set
            {
                this.combatStatisticsContainer.AllCombatStatistics = value;
            }
        }

        public ICombatStatistics CurrentCombatStatistics
        {
            get
            {
                return this.combatStatisticsContainer.CurrentCombatStatistics;
            }

            set
            {
                this.combatStatisticsContainer.CurrentCombatStatistics = value;
            }
        }

        public void CurrentCombatStatisticsChanged(ICombatStatistics combatStatistics)
        {
            Guard.WhenArgument(combatStatistics, nameof(ICombatStatistics)).IsNull().Throw();

            var currentCombatStatisticsChangedEventArgs = this.currentCombatStatisticsChangedEventArgsFactory.CreateCurrentCombatStatisticsChangedEventArgs(combatStatistics);

            this.OnCurrentCombatStatisticsChanged?.Invoke(null, currentCombatStatisticsChangedEventArgs);
        }
    }
}
