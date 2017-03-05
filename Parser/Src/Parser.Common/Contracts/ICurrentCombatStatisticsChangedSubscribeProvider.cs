using Parser.Common.EventsArgs;

namespace Parser.Common.Contracts
{
    public interface ICurrentCombatStatisticsChangedSubscribeProvider : IGenericSubscribeProvider<CurrentCombatStatisticsChangedEventArgs>
    {
    }
}
