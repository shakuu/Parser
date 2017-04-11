using System;
using System.Collections.Generic;

using Parser.Common.EventsArgs;

namespace Parser.Common.Contracts
{
    public interface ICombatStatisticsContainer
    {
        ICollection<ICombatStatistics> AllCombatStatistics { get; set; }

        ICombatStatistics CurrentCombatStatistics { get; set; }

        event EventHandler<CurrentCombatStatisticsChangedEventArgs> OnCurrentCombatChanged;
    }
}
