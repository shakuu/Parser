using System;

namespace Parser.Common.Contracts
{
    public interface IFinalizedCombatStatistics
    {
        string CharacterName { get; set; }

        decimal DamageDone { get; set; }

        decimal DamagePerSecond { get; set; }

        decimal DamageTaken { get; set; }

        decimal DamageTakenPerSecond { get; set; }

        TimeSpan CombatDuration { get; set; }
    }
}
