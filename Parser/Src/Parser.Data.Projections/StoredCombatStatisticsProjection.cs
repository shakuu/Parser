using System;

namespace Parser.Data.Projections
{
    public class StoredCombatStatisticsProjection
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? ParserUserId { get; set; }

        public virtual ParserUserProjection ParserUser { get; set; }

        public string CharacterName { get; set; }

        public double CombatDurationInSeconds { get; set; }

        public double DamageDone { get; set; }

        public double DamageDonePerSecond { get; set; }

        public double DamageTaken { get; set; }

        public double DamageTakenPerSecond { get; set; }
    }
}
