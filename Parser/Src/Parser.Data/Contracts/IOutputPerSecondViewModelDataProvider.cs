using System.Collections.Generic;

using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.Contracts
{
    public interface IOutputPerSecondViewModelDataProvider
    {
        IList<OutputPerSecondViewModel> GetTopDamageOnPage(int pageNumber, int pageSize);

        IList<OutputPerSecondViewModel> GetTopHealingOnPage(int pageNumber, int pageSize);
    }
}
