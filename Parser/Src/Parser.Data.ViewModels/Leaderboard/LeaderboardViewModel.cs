using System.Collections.Generic;

namespace Parser.Data.ViewModels.Leaderboard
{
    public class LeaderboardViewModel
    {
        public int PageNumber { get; private set; }

        public IList<OutputPerSecondViewModel> OutputPerSecondViewModels { get; private set; }

        public double MaximumHealingDonePerSecond { get; private set; }
    }
}
