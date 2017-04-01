using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Services.Contracts
{
    public interface ILeaderboardService
    {
        LeaderboardViewModel GetTopDamageOnPage(int pageNumber);

        LeaderboardViewModel GetTopHealingOnPage(int pageNumber);
    }
}
