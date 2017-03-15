using System.Collections.Generic;

namespace Parser.Data.ViewModels.Leaderboard
{
    public class DamageViewModel
    {
        IEnumerable<DamageDonePerSecondViewModel> DamageDonePerSecondViewModels { get; set; }
    }
}
