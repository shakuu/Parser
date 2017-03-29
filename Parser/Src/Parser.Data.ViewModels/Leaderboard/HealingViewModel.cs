using System.Collections.Generic;

namespace Parser.Data.ViewModels.Leaderboard
{
    public class HealingViewModel
    {
        public HealingViewModel(int pageNumber, double maximumHealingDonePerSecond, IList<HealingDonePerSecondViewModel> healingDonePerSecondViewModels)
        {
            this.PageNumber = pageNumber;
            this.MaximumHealingDonePerSecond = maximumHealingDonePerSecond;
            this.HealingDonePerSecondViewModels = healingDonePerSecondViewModels;
        }

        public HealingViewModel(int pageNumber, IList<HealingDonePerSecondViewModel> healingDonePerSecondViewModels)
        {
            this.PageNumber = pageNumber;
            this.HealingDonePerSecondViewModels = healingDonePerSecondViewModels;

            if (this.HealingDonePerSecondViewModels.Count > 0)
            {
                this.MaximumHealingDonePerSecond = this.HealingDonePerSecondViewModels[0]?.HealingDonePerSecond ?? 0;
            }
            else
            {
                this.MaximumHealingDonePerSecond = 0;
            }

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
                if (percentage < 0)
                {
                    percentage = 0;
                }

                viewModel.PercentageOfBest = percentage;
            }
        }
    }
}
