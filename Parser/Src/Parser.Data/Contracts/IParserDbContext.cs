using System;
using System.Data.Entity;

using Parser.Data.Models;

namespace Parser.Data.Contracts
{
    public interface IParserDbContext
    {
        IDbSet<ParserUser> ParserUsers { get; set; }

        IDbSet<StoredCombatStatistics> StoredCombatStatistics { get; set; }

        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
