using System.Collections.Generic;

namespace Parser.Data.ViewModels.Leaderboard
{
    public class LeaderboardViewModel
    {
        public LeaderboardViewModel(int pageNumber, IEnumerable<OutputPerSecondViewModel> outputPerSecondViewModels)
        {
            this.PageNumber = pageNumber;
            this.OutputPerSecondViewModels = outputPerSecondViewModels;
        }

        public int PageNumber { get; private set; }

        public IEnumerable<OutputPerSecondViewModel> OutputPerSecondViewModels { get; private set; }
    }
}
