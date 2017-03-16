using System.Collections.Generic;

namespace Parser.Data.ViewModels.Leaderboard
{
    public class DamageViewModel
    {
        public DamageViewModel(int pageNumber, IList<DamageDonePerSecondViewModel> damageDonePerSecondViewModels)
        {
            this.PageNumber = pageNumber;
            this.DamageDonePerSecondViewModels = damageDonePerSecondViewModels;
        }

        public IList<DamageDonePerSecondViewModel> DamageDonePerSecondViewModels { get; private set; }

        public int PageNumber { get; private set; }
    }
}
