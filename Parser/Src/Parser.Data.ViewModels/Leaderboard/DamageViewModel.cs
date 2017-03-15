using System.Collections.Generic;

namespace Parser.Data.ViewModels.Leaderboard
{
    public class DamageViewModel
    {
        public DamageViewModel(IEnumerable<DamageDonePerSecondViewModel> damageDonePerSecondViewModels)
        {
            this.DamageDonePerSecondViewModels = damageDonePerSecondViewModels;
        }

        public IEnumerable<DamageDonePerSecondViewModel> DamageDonePerSecondViewModels { get; private set; }
    }
}
