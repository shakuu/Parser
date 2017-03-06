using System;
using System.Collections.Generic;

namespace Parser.Data.Projections
{
    public class ParserUserProjection
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Username { get; set; }

        public virtual ICollection<StoredCombatStatisticsProjection> StoredCombatStatistics { get; set; }
    }
}
