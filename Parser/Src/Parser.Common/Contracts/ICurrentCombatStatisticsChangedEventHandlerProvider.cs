using Parser.Common.EventsArgs;

namespace Parser.Common.Contracts
{
    public interface ICurrentCombatStatisticsChangedEventHandlerProvider : IGenericEventHandlerProvider<CurrentCombatStatisticsChangedEventArgs>, ICurrentCombatStatisticsChangedSubscribeProvider
    {
    }
}
