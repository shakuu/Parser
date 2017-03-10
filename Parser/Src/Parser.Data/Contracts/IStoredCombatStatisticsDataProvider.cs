using Parser.Data.ViewModels;

namespace Parser.Data.Contracts
{
    public interface IStoredCombatStatisticsDataProvider
    {
        StoredCombatStatisticsViewModel CreateStoredCombatStatistics(StoredCombatStatisticsViewModel model);
    }
}
