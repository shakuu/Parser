using System;

namespace Parser.Data.ViewModels.Leaderboard
{
    public class DamageDonePerSecondViewModel
    {
        public Guid Id { get; set; }

        public string CharacterName { get; set; }

        public double DamageDonePerSecond { get; set; }

        public string SvgString { get; set; }
    }
}
