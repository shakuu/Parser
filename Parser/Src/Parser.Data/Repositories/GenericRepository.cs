using System.Data.Entity;

using Bytes2you.Validation;

using Parser.Data.Contracts;

namespace Parser.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbSet<T> entities;

        public GenericRepository(IParserDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, nameof(IParserDbContext)).IsNull().Throw();

            this.entities = dbContext.Set<T>();
        }

        public T Create(T entity)
        {
            Guard.WhenArgument(entity, nameof(entity)).IsNull().Throw();

            this.entities.Add(entity);

            return entity;
        }
    }
}
