using System.Collections.Generic;

using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.ViewModels.Factories
{
    public interface IHealingViewModelFactory
    {
        HealingViewModel CreateHealingViewModell(int pageNumber, IList<HealingDonePerSecondViewModel> healingDonePerSecondViewModels);
    }
}
