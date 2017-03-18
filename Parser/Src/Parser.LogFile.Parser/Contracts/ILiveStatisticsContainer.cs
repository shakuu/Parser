using System;

namespace Parser.LogFile.Parser.Contracts
{
    public interface ILiveStatisticsContainer
    {
        string CharacterName { get; set; }

        TimeSpan CombatDuration { get; set; }

        double DamageDonePerSecond { get; set; }

        double HealingDonePerSecond { get; set; }
    }
}
