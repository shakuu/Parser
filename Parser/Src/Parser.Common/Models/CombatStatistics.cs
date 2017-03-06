using System;

using Parser.Common.Contracts;

namespace Parser.Common.Models
{
    public class CombatStatistics : ICombatStatistics
    {
        public DateTime EnterCombatTime { get; set; }

        public DateTime ExitCombatTime { get; set; }

        public double DamageDone { get; set; }

        public double HealingDone { get; set; }

        public bool IsCompleted { get; set; }
    }
}
