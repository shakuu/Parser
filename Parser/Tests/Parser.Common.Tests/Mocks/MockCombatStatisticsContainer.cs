using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.Common.Models;

namespace Parser.Common.Tests.Mocks
{
    internal class MockCombatStatisticsContainer : CombatStatisticsContainer
    {
        public MockCombatStatisticsContainer(ICurrentCombatStatisticsChangedEventHandlerProvider currentCombatStatisticsChangedEventHandlerProvider, ICurrentCombatStatisticsChangedEventArgsFactory currentCombatStatisticsChangedEventArgsFactory)
            : base(currentCombatStatisticsChangedEventHandlerProvider, currentCombatStatisticsChangedEventArgsFactory)
        {
        }

        internal new ICombatStatistics MockingCurrentCombatStatistics { get { return base.MockingCurrentCombatStatistics; } set { base.MockingCurrentCombatStatistics = value; } }

        internal bool CurrentCombatStatisticsChangedMethodIsCalled { get; private set; }

        internal ICombatStatistics CurrentCombatStatisticsChangedMethodICombatStatisticsParameter { get; private set; }

        internal void MockedCurrentCombatStatisticsChanged(ICombatStatistics combatStatistics)
        {
            this.CurrentCombatStatisticsChanged(combatStatistics);
        }

        protected override void CurrentCombatStatisticsChanged(ICombatStatistics combatStatistics)
        {
            base.CurrentCombatStatisticsChanged(combatStatistics);
            this.CurrentCombatStatisticsChangedMethodIsCalled = true;
            this.CurrentCombatStatisticsChangedMethodICombatStatisticsParameter = combatStatistics;
        }
    }
}
