using System.Collections.Generic;

using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.ViewModels.Factories
{
    public interface IDamageViewModelFactory
    {
        DamageViewModel CreateDamageViewModel(IEnumerable<DamageDonePerSecondViewModel> damageDonePerSecondViewModels);
    }
}
