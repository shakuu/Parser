using System.Collections.Generic;

using Parser.Common.Contracts;

namespace Parser.Common.Models
{
    public class CombatStatisticsContainer : ICombatStatisticsContainer
    {
        public CombatStatisticsContainer()
        {
            this.AllCombatStatistics = new LinkedList<ICombatStatistics>();
        }

        public ICollection<ICombatStatistics> AllCombatStatistics { get; set; }

        public ICombatStatistics CurrentCombatStatistics { get; set; }
    }
}
