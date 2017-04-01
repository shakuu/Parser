using System.Collections.Generic;

using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.ViewModels.Factories
{
    public interface ILeaderboardViewModelFactory
    {
        LeaderboardViewModel CreateLeaderboardViewModel(int pageNumber, IEnumerable<OutputPerSecondViewModel> outputPerSecondViewModels);
    }
}
