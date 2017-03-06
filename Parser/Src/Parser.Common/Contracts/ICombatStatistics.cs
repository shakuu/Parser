using System;

namespace Parser.Common.Contracts
{
    public interface ICombatStatistics
    {
        string CharacterName { get; set; }
        
        DateTime EnterCombatTime { get; set; }

        DateTime ExitCombatTime { get; set; }

        double DamageDone { get; set; }

        double DamageTaken { get; set; }

        double HealingDone { get; set; }

        bool IsCompleted { get; set; }
    }
}
