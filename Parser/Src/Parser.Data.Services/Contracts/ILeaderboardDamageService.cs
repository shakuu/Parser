using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Services.Contracts
{
    public interface ILeaderboardDamageService
    {
        DamageViewModel GetTopStoredCombatStatisticsOnPage(int pageNumber);
    }
}
