using Bytes2you.Validation;

using Parser.Data.Contracts;

namespace Parser.Data.BusinessTransactions
{
    /// <summary>
    /// Unit Of Work is a less catchy name!
    /// https://martinfowler.com/eaaCatalog/unitOfWork.html
    /// </summary>
    public class EntityFrameworkTransaction : IEntityFrameworkTransaction
    {
        private readonly IDbContext dbContext;

        public EntityFrameworkTransaction(IDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, nameof(IDbContext)).IsNull().Throw();

            this.dbContext = dbContext;
        }

        public void SaveChangesAsync()
        {
            this.dbContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public void Dispose()
        {
            // Do nothing.
        }
    }
}
