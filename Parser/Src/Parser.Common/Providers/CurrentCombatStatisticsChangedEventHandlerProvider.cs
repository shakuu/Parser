using Parser.Common.Contracts;
using Parser.Common.EventsArgs;

namespace Parser.Common.Providers
{
    public class CurrentCombatStatisticsChangedEventHandlerProvider : GenericEventHandlerProvider<CurrentCombatStatisticsChangedEventArgs>, ICurrentCombatStatisticsChangedEventHandlerProvider
    {
    }
}
