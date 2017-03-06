using System;

using Parser.Common.Contracts;

namespace Parser.Common.Models
{
    public class FinalizedCombatStatistics : IFinalizedCombatStatistics
    {
        public string CharacterName { get; set; }

        public double CombatDurationInSeconds { get; set; }

        public double DamageDone { get; set; }

        public double DamageDonePerSecond { get; set; }

        public double DamageTaken { get; set; }

        public double DamageTakenPerSecond { get; set; }
    }
}
