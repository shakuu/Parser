using System;

using Parser.Common.EventsArgs;

namespace Parser.Common.Contracts
{
    public interface ICurrentCombatStatisticsChangedNotification
    {
        event EventHandler<CurrentCombatStatisticsChangedEventArgs> OnCurrentCombatStatisticsChanged;
    }
}
