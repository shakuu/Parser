using System.Collections.Generic;

using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.ViewModels.Factories
{
    public interface IDamageViewModelFactory
    {
        DamageViewModel CreateDamageViewModel(int pageNumber, IList<DamageDonePerSecondViewModel> damageDonePerSecondViewModels);

        DamageViewModel CreateDamageViewModel(int pageNumber, double maximumDamageDonePerSecond, IList<DamageDonePerSecondViewModel> damageDonePerSecondViewModels);
    }
}
