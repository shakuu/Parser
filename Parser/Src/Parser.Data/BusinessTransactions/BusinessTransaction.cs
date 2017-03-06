using Bytes2you.Validation;

using Parser.Data.Contracts;

namespace Parser.Data.BusinessTransactions
{
    public class BusinessTransaction : IBusinessTransaction
    {
        private readonly IDbContext dbContext;

        public BusinessTransaction(IDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, nameof(IDbContext)).IsNull().Throw();

            this.dbContext = dbContext;
        }

        public void CommitAsync()
        {
            this.dbContext.SaveChangesAsync();
        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }

        public void Dispose()
        {
            // Do nothing.
        }
    }
}
