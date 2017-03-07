using Bytes2you.Validation;

using Parser.Data.Contracts;

namespace Parser.Data.BusinessTransactions
{
    /// <summary>
    /// Unit Of Work is a less catchy name!
    /// https://martinfowler.com/eaaCatalog/unitOfWork.html
    /// </summary>
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
