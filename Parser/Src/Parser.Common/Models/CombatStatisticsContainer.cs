using System.Collections.Generic;

using Parser.Common.Contracts;

namespace Parser.Common.Models
{
    public class CombatStatisticsContainer : ICombatStatisticsContainer
    {
        public IEnumerable<ICombatStatistics> AllComabtStatistics { get; set; }

        public ICombatStatistics CurrentComabtStatistics { get; set; }
    }
}
