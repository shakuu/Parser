using System;

using Moq;
using NUnit.Framework;

using Parser.Data.Contracts;
using Parser.Data.Repositories;
using Parser.Data.Tests.Mocks;

namespace Parser.Data.Tests.RepositoriesTests.EntityFrameworkRepositoryTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreatesCorrectIEntityFrameworkRepositoryInstance_WhenParametersAreValid()
        {
            // Arrange
            var parserDbContext = new Mock<IParserDbContext>();

            // Act
            var actualInstance = new EntityFrameworkRepository<MockDbModel>(parserDbContext.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<IEntityFrameworkRepository<MockDbModel>>());
        }

        [Test]
        public void InvokesIParserDbContext_SetMethodWithOnceCorrectParameter()
        {
            // Arrange
            var parserDbContext = new Mock<IParserDbContext>();

            // Act
            var actualInstance = new EntityFrameworkRepository<MockDbModel>(parserDbContext.Object);

            // Assert
            parserDbContext.Verify(c => c.Set<MockDbModel>(), Times.Once);
        }

        [Test]
        public void ThrowArgumentNullException_WhenIParserDbContextIsNull()
        {
            // Arrange
            IParserDbContext parserDbContext = null;

            // Act & Assert
            Assert.That(
                () => new EntityFrameworkRepository<MockDbModel>(parserDbContext),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IParserDbContext)));
        }
    }
}
