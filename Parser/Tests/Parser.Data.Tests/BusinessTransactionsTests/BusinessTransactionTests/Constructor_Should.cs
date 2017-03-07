using System;

using Moq;
using NUnit.Framework;

using Parser.Data.BusinessTransactions;
using Parser.Data.Contracts;

namespace Parser.Data.Tests.BusinessTransactionsTests.BusinessTransactionTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectIBusinessTransactionInstance_WhenParametersAreCorrect()
        {
            // Arrange
            var dbContext = new Mock<IDbContext>();

            // Act
            var actualInstance = new BusinessTransaction(dbContext.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<IBusinessTransaction>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIDbContextParameterIsNull()
        {
            // Arrange
            IDbContext dbContext = null;

            // Act & Assert
            Assert.That(
                () => new BusinessTransaction(dbContext),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IDbContext)));
        }
    }
}
