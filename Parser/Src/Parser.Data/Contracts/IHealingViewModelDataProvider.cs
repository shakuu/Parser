using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Contracts
{
    public interface IHealingViewModelDataProvider
    {
        HealingViewModel GetHealingViewModelOnPage(int pageNumber, int pageSize);
    }
}
