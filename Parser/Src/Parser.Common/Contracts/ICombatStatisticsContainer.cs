using System.Collections.Generic;

namespace Parser.Common.Contracts
{
    public interface ICombatStatisticsContainer : ICurrentCombatStatisticsChangedNotification
    {
        string CharacterName { get; set; }

        ICollection<ICombatStatistics> AllCombatStatistics { get; set; }

        ICombatStatistics CurrentCombatStatistics { get; set; }
    }
}
