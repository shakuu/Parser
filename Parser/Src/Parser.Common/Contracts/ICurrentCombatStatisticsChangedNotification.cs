namespace Parser.Common.Contracts
{
    public interface ICurrentCombatStatisticsChangedNotification
    {
        ICurrentCombatStatisticsChangedEventHandlerProvider OnCurrentCombatStatisticsChanged { get; }
    }
}
