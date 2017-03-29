using System;

namespace Parser.Data.ViewModels.Live
{
    public class LiveStatisticsViewModel
    {
        public string CharacterName { get; set; }

        public TimeSpan CombatDuration { get; set; }

        public double DamageDonePerSecond { get; set; }

        public double HealingDonePerSecond { get; set; }
    }
}
