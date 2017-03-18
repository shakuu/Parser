using System;

namespace Parser.Data.ViewModels.Leaderboard
{
    public class HealingDonePerSecondViewModel
    {
        public Guid Id { get; set; }

        public string CharacterName { get; set; }

        public double HealingDonePerSecond { get; set; }

        public string SvgString { get; set; }

        public int PercentageOfBest { get; set; }
    }
}
