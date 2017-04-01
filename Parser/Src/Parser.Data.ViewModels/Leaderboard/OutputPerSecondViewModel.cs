using System;

namespace Parser.Data.ViewModels.Leaderboard
{
    public class OutputPerSecondViewModel
    {
        public Guid Id { get; set; }

        public string CharacterName { get; set; }

        public double OutputPerSecond { get; set; }

        public string SvgString { get; set; }

        public int PercentageOfBest { get; set; }
    }
}
