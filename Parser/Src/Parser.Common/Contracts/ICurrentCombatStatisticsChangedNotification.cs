namespace Parser.Common.Contracts
{
    public interface ICurrentCombatStatisticsChangedNotification
    {
        ICurrentCombatStatisticsChangedSubscribeProvider OnCurrentCombatStatisticsChanged { get; }
    }
}
