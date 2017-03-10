using Moq;
using NUnit.Framework;

using Parser.Data.BusinessTransactions;
using Parser.Data.Contracts;

namespace Parser.Data.Tests.BusinessTransactionsTests.BusinessTransactionTests
{
    [TestFixture]
    public class Commit_Should
    {
        [Test]
        public void InvokeIDbContext_SaveChangesMethodOnce()
        {
            // Arrange
            var dbContext = new Mock<IDbContext>();

            var businessTransaction = new EntityFrameworkTransaction(dbContext.Object);

            // Act
            businessTransaction.SaveChanges();

            // Assert
            dbContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
