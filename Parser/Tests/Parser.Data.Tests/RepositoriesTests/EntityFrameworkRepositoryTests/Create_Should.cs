using System;
using System.Data.Entity;

using Moq;
using NUnit.Framework;

using Parser.Data.Contracts;
using Parser.Data.Repositories;
using Parser.Data.Tests.Mocks;

namespace Parser.Data.Tests.RepositoriesTests.EntityFrameworkRepositoryTests
{
    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenTEntityParameterIsNull()
        {
            // Arrange
            var parserDbContext = new Mock<IParserDbContext>();

            var entities = new Mock<IDbSet<MockDbModel>>();
            parserDbContext.Setup(c => c.Set<MockDbModel>()).Returns(entities.Object);

            var entityFrameworkRepository = new EntityFrameworkRepository<MockDbModel>(parserDbContext.Object);

            MockDbModel entity = null;

            // Act & Assert
            Assert.That(
                () => entityFrameworkRepository.Create(entity),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(entity)));
        }

        [Test]
        public void InvokeIQueryableTEntity_AddMethodOnceWithCorrectParameter()
        {
            // Arrange
            var parserDbContext = new Mock<IParserDbContext>();

            var entities = new Mock<IDbSet<MockDbModel>>();
            parserDbContext.Setup(c => c.Set<MockDbModel>()).Returns(entities.Object);

            var entityFrameworkRepository = new EntityFrameworkRepository<MockDbModel>(parserDbContext.Object);

            var entity = new MockDbModel();

            // Act
            entityFrameworkRepository.Create(entity);

            // Assert
            entities.Verify(e => e.Add(entity), Times.Once);
        }

        [Test]
        public void ReturnCorrectTEntityInstance()
        {
            // Arrange
            var parserDbContext = new Mock<IParserDbContext>();

            var entities = new Mock<IDbSet<MockDbModel>>();
            parserDbContext.Setup(c => c.Set<MockDbModel>()).Returns(entities.Object);

            var entityFrameworkRepository = new EntityFrameworkRepository<MockDbModel>(parserDbContext.Object);

            var expectedReturnerIQueryableTEntityInstance = entities.Object;

            var entity = new MockDbModel();

            // Act
            var actualReturnerTEntityInstance = entityFrameworkRepository.Create(entity);

            // Assert
            Assert.That(actualReturnerTEntityInstance, Is.SameAs(entity));
        }
    }
}
