using System;
using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.EventsArgs;
using Parser.Common.Factories;

namespace Parser.Common.Models
{
    public class CombatStatisticsContainer : ICombatStatisticsContainer
    {
        public event EventHandler<CurrentCombatStatisticsChangedEventArgs> OnCurrentCombatChanged;

        private readonly ICurrentCombatStatisticsChangedEventArgsFactory currentCombatStatisticsChangedEventArgsFactory;

        private ICombatStatistics currentCombatStatistics;

        public CombatStatisticsContainer(ICurrentCombatStatisticsChangedEventArgsFactory currentCombatStatisticsChangedEventArgsFactory)
        {
            Guard.WhenArgument(currentCombatStatisticsChangedEventArgsFactory, nameof(ICurrentCombatStatisticsChangedEventArgsFactory)).IsNull().Throw();

            this.currentCombatStatisticsChangedEventArgsFactory = currentCombatStatisticsChangedEventArgsFactory;

            this.AllCombatStatistics = new LinkedList<ICombatStatistics>();
        }

        public ICollection<ICombatStatistics> AllCombatStatistics { get; set; }

        public ICombatStatistics CurrentCombatStatistics
        {
            get
            {
                return this.currentCombatStatistics;
            }

            set
            {
                var previousCombatStatistics = this.CurrentCombatStatistics;

                this.currentCombatStatistics = value;

                if (previousCombatStatistics != null)
                {
                    this.CurrentCombatChanged(previousCombatStatistics);
                }
            }
        }

        protected ICombatStatistics MockingCurrentCombatStatistics { get { return this.currentCombatStatistics; } set { this.currentCombatStatistics = value; } }

        protected virtual void CurrentCombatChanged(ICombatStatistics combatStatistics)
        {
            Guard.WhenArgument(combatStatistics, nameof(ICombatStatistics)).IsNull().Throw();

            var currentCombatStatisticsChangedEventArgs = this.currentCombatStatisticsChangedEventArgsFactory.CreateCurrentCombatStatisticsChangedEventArgs(combatStatistics);

            this.OnCurrentCombatChanged?.Invoke(this, currentCombatStatisticsChangedEventArgs);
        }
    }
}
