using System;
using System.Data.Entity;
using System.Linq;

using Bytes2you.Validation;

using Parser.Data.Contracts;
using Parser.Data.Models.Contracts;

namespace Parser.Data.Repositories
{
    public class EntityFrameworkRepository<TEntity> : IEntityFrameworkRepository<TEntity>
        where TEntity : class, IDbModel
    {
        private readonly IParserDbContext parserDbContext;
        private readonly IDbSet<TEntity> entities;

        public EntityFrameworkRepository(IParserDbContext parserDbContext)
        {
            Guard.WhenArgument(parserDbContext, nameof(IParserDbContext)).IsNull().Throw();

            this.parserDbContext = parserDbContext;

            this.entities = this.parserDbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Entities
        {
            get
            {
                return this.entities;
            }
        }

        public TEntity Create(TEntity entity)
        {
            Guard.WhenArgument(entity, nameof(entity)).IsNull().Throw();

            this.entities.Add(entity);

            return entity;
        }

        public TEntity Find(object id)
        {
            Guard.WhenArgument(id, nameof(id)).IsNull().Throw();

            return this.entities.FirstOrDefault(e => e.Id == (Guid)id);
        }
    }
}
