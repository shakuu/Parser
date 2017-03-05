using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.Factories;

namespace Parser.Common.Models
{
    public class CombatStatisticsContainer : ICombatStatisticsContainer
    {
        private readonly ICurrentCombatStatisticsChangedEventHandlerProvider currentCombatStatisticsChangedEventHandlerProvider;
        private readonly ICurrentCombatStatisticsChangedEventArgsFactory currentCombatStatisticsChangedEventArgsFactory;

        private ICombatStatistics currentCombatStatistics;

        public CombatStatisticsContainer(ICurrentCombatStatisticsChangedEventHandlerProvider currentCombatStatisticsChangedEventHandlerProvider, ICurrentCombatStatisticsChangedEventArgsFactory currentCombatStatisticsChangedEventArgsFactory)
        {
            Guard.WhenArgument(currentCombatStatisticsChangedEventHandlerProvider, nameof(ICurrentCombatStatisticsChangedEventHandlerProvider)).IsNull().Throw();
            Guard.WhenArgument(currentCombatStatisticsChangedEventArgsFactory, nameof(ICurrentCombatStatisticsChangedEventArgsFactory)).IsNull().Throw();

            this.currentCombatStatisticsChangedEventHandlerProvider = currentCombatStatisticsChangedEventHandlerProvider;
            this.currentCombatStatisticsChangedEventArgsFactory = currentCombatStatisticsChangedEventArgsFactory;

            this.AllCombatStatistics = new LinkedList<ICombatStatistics>();
        }

        public ICurrentCombatStatisticsChangedEventHandlerProvider OnCurrentCombatStatisticsChanged { get { return this.currentCombatStatisticsChangedEventHandlerProvider; } }

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
                    this.CurrentCombatStatisticsChanged(previousCombatStatistics);
                }
            }
        }

        protected void CurrentCombatStatisticsChanged(ICombatStatistics combatStatistics)
        {
            Guard.WhenArgument(combatStatistics, nameof(ICombatStatistics)).IsNull().Throw();

            var currentCombatStatisticsChangedEventArgs = this.currentCombatStatisticsChangedEventArgsFactory.CreateCurrentCombatStatisticsChangedEventArgs(combatStatistics);

            this.currentCombatStatisticsChangedEventHandlerProvider.Raise(this, currentCombatStatisticsChangedEventArgs);
        }
    }
}
