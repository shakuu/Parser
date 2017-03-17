using System.Collections.Generic;

namespace Parser.Data.ViewModels.Leaderboard
{
    public class DamageViewModel
    {
        public DamageViewModel(int pageNumber, IList<DamageDonePerSecondViewModel> damageDonePerSecondViewModels)
        {
            this.PageNumber = pageNumber;
            this.DamageDonePerSecondViewModels = damageDonePerSecondViewModels;
            this.MaximumDamageDonePerSecond = this.DamageDonePerSecondViewModels[0]?.DamageDonePerSecond ?? 0;

            this.CalculatePercentageOfBestPropertyForViewModels();
        }

        public int PageNumber { get; private set; }

        public IList<DamageDonePerSecondViewModel> DamageDonePerSecondViewModels { get; private set; }

        public double MaximumDamageDonePerSecond { get; private set; }

        private void CalculatePercentageOfBestPropertyForViewModels()
        {
            foreach (var viewModel in this.DamageDonePerSecondViewModels)
            {
                var percentage = (int)(viewModel.DamageDonePerSecond / this.MaximumDamageDonePerSecond * 100);

                viewModel.PercentageOfBest = percentage;
            }
        }
    }
}
