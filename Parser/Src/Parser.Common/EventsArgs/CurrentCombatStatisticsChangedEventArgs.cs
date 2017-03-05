using System;

using Bytes2you.Validation;

using Parser.Common.Contracts;

namespace Parser.Common.EventsArgs
{
    public class CurrentCombatStatisticsChangedEventArgs : EventArgs
    {
        public CurrentCombatStatisticsChangedEventArgs(ICombatStatistics combatStatistics)
        {
            Guard.WhenArgument(combatStatistics, nameof(ICombatStatistics)).IsNull().Throw();

            this.CombatStatistics = combatStatistics;
        }

        public ICombatStatistics CombatStatistics { get; private set; }
    }
}
