using Parser.Data.Models.Contracts;

namespace Parser.Data.Contracts
{
    public interface IGenericRepository<TProjection, TEntity>
        where TEntity : IDbModel
        where TProjection : class
    {
        TProjection Create(TProjection projection);
    }
}
