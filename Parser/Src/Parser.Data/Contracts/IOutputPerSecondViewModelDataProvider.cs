using System.Collections.Generic;

using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Contracts
{
    public interface IOutputPerSecondViewModelDataProvider
    {
        IList<OutputPerSecondViewModel> GetTopDamageOnPageInDescendingOrder(int pageNumber, int pageSize);

        IList<OutputPerSecondViewModel> GetTopHealingOnPageInDescendingOrder(int pageNumber, int pageSize);
    }
}
