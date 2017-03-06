using System;

namespace Parser.Common.Contracts
{
    public interface ICombatStatistics
    {
        DateTime EnterCombatTime { get; set; }

        DateTime ExitCombatTime { get; set; }

        double DamageDone { get; set; }

        double HealingDone { get; set; }

        bool IsCompleted { get; set; }
    }
}
