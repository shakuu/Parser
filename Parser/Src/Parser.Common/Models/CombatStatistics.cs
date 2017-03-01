using Parser.Common.Contracts;

namespace Parser.Common.Models
{
    public class CombatStatistics : ICombatStatistics
    {
        public decimal DamageDone { get; set; }

        public decimal HealingDone { get; set; }

        public bool IsCompleted { get; set; }
    }
}
