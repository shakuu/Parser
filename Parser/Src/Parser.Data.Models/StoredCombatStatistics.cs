using System;

using Parser.Common.Contracts;
using Parser.Data.Models.Contracts;

namespace Parser.Data.Models
{
    public class StoredCombatStatistics : IDbModel, IFinalizedCombatStatistics
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public string CharacterName { get; set; }

        public double CombatDurationInSeconds { get; set; }

        public double DamageDone { get; set; }

        public double DamageDonePerSecond { get; set; }

        public double DamageTaken { get; set; }

        public double DamageTakenPerSecond { get; set; }
    }
}
