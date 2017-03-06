using System.Data.Entity;

using AutoMapper;

using Bytes2you.Validation;

using Parser.Data.Contracts;
using Parser.Data.Models.Contracts;

namespace Parser.Data.Repositories
{
    public class GenericRepository<TProjection, TEntity> : IGenericRepository<TProjection, TEntity>
        where TEntity : class, IDbModel
        where TProjection : class
    {
        private readonly IMapper objectMapper;
        private readonly IDbSet<TEntity> entities;

        public GenericRepository(IParserDbContext dbContext, IMapper objectMapper)
        {
            Guard.WhenArgument(dbContext, nameof(IParserDbContext)).IsNull().Throw();
            Guard.WhenArgument(objectMapper, nameof(IMapper)).IsNull().Throw();

            this.objectMapper = objectMapper;

            this.entities = dbContext.Set<TEntity>();
        }

        public TProjection Create(TProjection projection)
        {
            Guard.WhenArgument(projection, nameof(TProjection)).IsNull().Throw();

            var entity = this.objectMapper.Map<TEntity>(projection);

            this.entities.Add(entity);

            return projection;
        }
    }
}
