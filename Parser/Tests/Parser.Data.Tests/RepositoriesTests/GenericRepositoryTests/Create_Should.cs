using System;
using System.Data.Entity;

using Moq;
using NUnit.Framework;

using Parser.Data.Contracts;
using Parser.Data.Repositories;
using Parser.Data.Tests.Mocks;

namespace Parser.Data.Tests.RepositoriesTests.GenericRepositoryTests
{
    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenEntityParameterIsNull()
        {
            // Arrange
            var dbContext = new Mock<IParserDbContext>();

            var genericRepository = new GenericRepository<MockDbModel>(dbContext.Object);

            MockDbModel entity = null;

            // Act & Assert
            Assert.That(
                () => genericRepository.Create(entity),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(entity)));

        }

        [Test]
        public void InvokeIDbSet_AddMethodOnceWithCorrectParameter()
        {
            // Arrange
            var dbContext = new Mock<IParserDbContext>();

            var dbSet = new Mock<IDbSet<MockDbModel>>();
            dbContext.Setup(c => c.Set<MockDbModel>()).Returns(dbSet.Object);

            var genericRepository = new GenericRepository<MockDbModel>(dbContext.Object);

            var entity = new MockDbModel();

            // Act
            genericRepository.Create(entity);

            // Assert
            dbSet.Verify(s => s.Add(entity), Times.Once);
        }

        [Test]
        public void ReturnCorrectEntityInstance()
        {
            // Arrange
            var dbContext = new Mock<IParserDbContext>();

            var dbSet = new Mock<IDbSet<MockDbModel>>();
            dbContext.Setup(c => c.Set<MockDbModel>()).Returns(dbSet.Object);

            var genericRepository = new GenericRepository<MockDbModel>(dbContext.Object);

            var entity = new MockDbModel();

            // Act
            var actualReturnedEntity = genericRepository.Create(entity);

            // Assert
            Assert.That(actualReturnedEntity, Is.SameAs(entity));
        }
    }
}
