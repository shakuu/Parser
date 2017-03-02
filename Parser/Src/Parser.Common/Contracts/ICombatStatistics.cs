using System;

namespace Parser.Common.Contracts
{
    public interface ICombatStatistics
    {
        DateTime EnterCombatTime { get; set; }

        DateTime ExitCombatTime { get; set; }

        decimal DamageDone { get; set; }

        decimal HealingDone { get; set; }

        bool IsCompleted { get; set; }
    }
}
