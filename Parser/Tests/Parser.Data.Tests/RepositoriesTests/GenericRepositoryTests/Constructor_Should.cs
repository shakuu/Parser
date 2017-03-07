using System;

using Moq;
using NUnit.Framework;

using Parser.Data.Contracts;
using Parser.Data.Repositories;
using Parser.Data.Tests.Mocks;

namespace Parser.Data.Tests.RepositoriesTests.GenericRepositoryTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectIGenericRepositoryInstance_WhenParametersAreCorrect()
        {
            // Arrange
            var dbContext = new Mock<IParserDbContext>();

            // Act
            var actualInstance = new GenericRepository<MockDbModel>(dbContext.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<IGenericRepository<MockDbModel>>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIParserDbContextParameterIsNull()
        {
            // Arrange
            IParserDbContext dbContext = null;

            // Act & Assert
            Assert.That(
                () => new GenericRepository<MockDbModel>(dbContext),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IParserDbContext)));
        }
    }
}
