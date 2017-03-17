using System;

namespace Parser.Data.ViewModels
{
    public class StoredCombatStatisticsViewModel
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? ParserUserId { get; set; }

        public virtual ParserUserViewModel ParserUser { get; set; }

        public string CharacterName { get; set; }

        public double CombatDurationInSeconds { get; set; }

        public double DamageDone { get; set; }

        public double DamageDonePerSecond { get; set; }

        public double HealingDone { get; set; }

        public double HealingDonePerSecond { get; set; }

        public double DamageTaken { get; set; }

        public double DamageTakenPerSecond { get; set; }
    }
}
