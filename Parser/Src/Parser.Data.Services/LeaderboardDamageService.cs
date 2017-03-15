using Bytes2you.Validation;

using Parser.Data.Contracts;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Services
{
    public class LeaderboardDamageService : ILeaderboardDamageService
    {
        private readonly IDamageViewModelDataProvider damageViewModelDataProvider;

        public LeaderboardDamageService(IDamageViewModelDataProvider damageViewModelDataProvider)
        {
            Guard.WhenArgument(damageViewModelDataProvider, nameof(IDamageViewModelDataProvider)).IsNull().Throw();

            this.damageViewModelDataProvider = damageViewModelDataProvider;
        }

        public DamageViewModel GetTopStoredCombatStatisticsOnPage(int pageNumber)
        {
            return this.damageViewModelDataProvider.GetDamageViewModelOnPage(pageNumber);
        }
    }
}
