using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Services.Contracts
{
    public interface ILeaderboardHealingService
    {
        HealingViewModel GetTopStoredHealingOnPage(int pageNumber);
    }
}
