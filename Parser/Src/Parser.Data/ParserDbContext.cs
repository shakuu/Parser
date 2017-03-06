using System.Data.Entity;

using Parser.Common.Constants.Config;
using Parser.Data.Contracts;
using Parser.Data.Models;

namespace Parser.Data
{
    public class ParserDbContext : DbContext, IParserDbContext
    {
        public ParserDbContext()
            : base($"name={ConnectionStrings.ParserDbConnectionString}")
        {

        }

        public virtual IDbSet<ParserUser> ParserUsers { get; set; }

        public virtual IDbSet<StoredCombatStatistics> StoredCombatStatistics { get; set; }

        IDbSet<TEntity> IParserDbContext.Set<TEntity>()
        {
            return this.Set<TEntity>();
        }
    }
}
