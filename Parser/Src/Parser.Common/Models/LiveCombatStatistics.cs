using System;

using Parser.Common.Contracts;

namespace Parser.Common.Models
{
    public class LiveCombatStatistics : ILiveCombatStatistics
    {
        public string CharacterName { get; set; }

        public TimeSpan CombatDuration { get; set; }

        public double DamageDonePerSecond { get; set; }

        public double HealingDonePerSecond { get; set; }
    }
}
