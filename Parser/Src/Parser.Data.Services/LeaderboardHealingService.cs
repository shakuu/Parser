using Bytes2you.Validation;

using Parser.Data.Contracts;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Services
{
    public class LeaderboardHealingService : ILeaderboardHealingService
    {
        private readonly IHealingViewModelDataProvider healingViewModelDataProvider;

        public LeaderboardHealingService(IHealingViewModelDataProvider healingViewModelDataProvider)
        {
            Guard.WhenArgument(healingViewModelDataProvider, nameof(IHealingViewModelDataProvider)).IsNull().Throw();

            this.healingViewModelDataProvider = healingViewModelDataProvider;
        }

        public HealingViewModel GetTopStoredHealingOnPage(int pageNumber)
        {
            return this.healingViewModelDataProvider.GetHealingViewModelOnPage(pageNumber);
        }
    }
}
