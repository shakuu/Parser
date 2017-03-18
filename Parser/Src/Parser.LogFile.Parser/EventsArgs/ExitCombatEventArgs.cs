using System;

using Bytes2you.Validation;

using Parser.Common.Contracts;

namespace Parser.LogFileParser.EventsArgs
{
    public class ExitCombatEventArgs : EventArgs
    {
        public ExitCombatEventArgs(ICombatStatistics combatStatistics)
        {
            Guard.WhenArgument(combatStatistics, nameof(ICombatStatistics)).IsNull().Throw();

            this.CombatStatistics = combatStatistics;
        }

        public ICombatStatistics CombatStatistics { get; private set; }
    }
}
