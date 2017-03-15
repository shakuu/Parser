using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Contracts
{
    public interface IDamageViewModelDataProvider
    {
        DamageViewModel GetTopStoredCombatStatisticsByDamageDonePerSecondOnPage(int pageNumber);
    }
}
