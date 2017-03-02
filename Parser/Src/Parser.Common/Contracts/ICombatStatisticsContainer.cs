using System.Collections.Generic;

namespace Parser.Common.Contracts
{
    public interface ICombatStatisticsContainer
    {
        ICollection<ICombatStatistics> AllComabtStatistics { get; set; }

        ICombatStatistics CurrentComabtStatistics { get; set; }
    }
}
