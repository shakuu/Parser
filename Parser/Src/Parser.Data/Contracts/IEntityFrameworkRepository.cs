using System.Linq;

namespace Parser.Data.Contracts
{
    public interface IEntityFrameworkRepository<TEntity>
         where TEntity : class
    {
        IQueryable<TEntity> Entities { get; }

        TEntity Find(object id);

        TEntity Create(TEntity entity);
    }
}
