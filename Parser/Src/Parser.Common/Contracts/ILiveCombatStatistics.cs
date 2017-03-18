using System;

namespace Parser.Common.Contracts
{
    public interface ILiveCombatStatistics
    {
        string CharacterName { get; set; }

        TimeSpan CombatDuration { get; set; }

        double DamageDonePerSecond { get; set; }

        double HealingDonePerSecond { get; set; }
    }
}
