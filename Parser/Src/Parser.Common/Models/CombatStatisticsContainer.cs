using Parser.Common.Contracts;

namespace Parser.Common.Models
{
    public class CombatStatisticsContainer : ICombatStatisticsContainer
    {
        public decimal DamageDone { get; set; }

        public decimal HealingDone { get; set; }
    }
}
