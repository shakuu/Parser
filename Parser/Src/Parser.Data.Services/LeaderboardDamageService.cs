using System;

using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Services
{
    public class LeaderboardDamageService : ILeaderboardDamageService
    {
        public LeaderboardDamageService()
        {

        }

        public DamageViewModel GetTopStoredCombatStatisticsOnPage(int pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}
