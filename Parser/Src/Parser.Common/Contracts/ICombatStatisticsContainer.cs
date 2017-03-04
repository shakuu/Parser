using System.Collections.Generic;

namespace Parser.Common.Contracts
{
    public interface ICombatStatisticsContainer
    {
        ICollection<ICombatStatistics> AllCombatStatistics { get; set; }

        ICombatStatistics CurrentCombatStatistics { get; set; }
    }
}
