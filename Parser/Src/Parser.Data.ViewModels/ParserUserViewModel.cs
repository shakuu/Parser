using System;
using System.Collections.Generic;

namespace Parser.Data.ViewModels
{
    public class ParserUserViewModel
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Username { get; set; }

        public virtual ICollection<StoredCombatStatisticsViewModel> StoredCombatStatistics { get; set; }
    }
}
