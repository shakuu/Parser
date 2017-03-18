using System.Collections.Generic;

namespace Parser.Data.ViewModels.Leaderboard
{
    public class HealingViewModel
    {
        public HealingViewModel(int pageNumber, IList<HealingDonePerSecondViewModel> healingDonePerSecondViewModels)
        {
            this.PageNumber = pageNumber;
            this.HealingDonePerSecondViewModels = healingDonePerSecondViewModels;
            this.MaximumHealingDonePerSecond = this.HealingDonePerSecondViewModels[0]?.HealingDonePerSecond ?? 0;

            this.CalculatePercentageOfBestPropertyForViewModels();
        }

        public int PageNumber { get; private set; }

        public IList<HealingDonePerSecondViewModel> HealingDonePerSecondViewModels { get; private set; }

        public double MaximumHealingDonePerSecond { get; private set; }

        private void CalculatePercentageOfBestPropertyForViewModels()
        {
            foreach (var viewModel in this.HealingDonePerSecondViewModels)
            {
                var percentage = (int)(viewModel.HealingDonePerSecond / this.MaximumHealingDonePerSecond * 100);

                viewModel.PercentageOfBest = percentage;
            }
        }
    }
}
