using System;
using System.Linq;

namespace Parser.Data.Contracts
{
    public interface IEntityFrameworkRepository<TEntity>
         where TEntity : class
    {
        IQueryable<TEntity> Entities { get; }

        TEntity Find(Guid entityGuid);

        TEntity Create(TEntity entity);
    }
}
