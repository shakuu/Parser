using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Moq;
using NUnit.Framework;

using Parser.Data.Contracts;
using Parser.Data.Repositories;
using Parser.Data.Tests.Mocks;

namespace Parser.Data.Tests.RepositoriesTests.EntityFrameworkRepositoryTests
{
    [TestFixture]
    public class Find_Should
    {
        [Test]
        public void ThrowArgumentException_WhenGuidParameterIsEqualToGuidDefaultValue()
        {
            // Arrange
            var parserDbContext = new Mock<IParserDbContext>();

            var entities = new Mock<IDbSet<MockDbModel>>();
            parserDbContext.Setup(c => c.Set<MockDbModel>()).Returns(entities.Object);

            var entityFrameworkRepository = new EntityFrameworkRepository<MockDbModel>(parserDbContext.Object);

            var entityGuid = default(Guid);

            // Act & Assert
            Assert.That(
                () => entityFrameworkRepository.Find(entityGuid),
                Throws.InstanceOf<ArgumentException>().With.Message.Contains(nameof(entityGuid)));
        }

        [Test]
        public void ReturnNull_WhenRequestedGuidDoesNotExist()
        {
            // Arrange
            var parserDbContext = new Mock<IParserDbContext>();

            var entities = new Mock<IDbSet<MockDbModel>>();
            parserDbContext.Setup(c => c.Set<MockDbModel>()).Returns(entities.Object);

            var entityFrameworkRepository = new EntityFrameworkRepository<MockDbModel>(parserDbContext.Object);

            var fakeData = new HashSet<MockDbModel>()
            {
                new MockDbModel() { Id = Guid.NewGuid() },
                new MockDbModel() { Id = Guid.NewGuid() },
                new MockDbModel() { Id = Guid.NewGuid() },
                new MockDbModel() { Id = Guid.NewGuid() },
                new MockDbModel() { Id = Guid.NewGuid() }
            };

            var fakeDataQueryable = fakeData.AsQueryable();

            entities.As<IQueryable>().Setup(e => e.GetEnumerator()).Returns(fakeDataQueryable.GetEnumerator());
            entities.As<IQueryable>().Setup(e => e.ElementType).Returns(fakeDataQueryable.ElementType);
            entities.As<IQueryable>().Setup(e => e.Expression).Returns(fakeDataQueryable.Expression);
            entities.As<IQueryable>().Setup(e => e.Provider).Returns(fakeDataQueryable.Provider);

            var entityGuid = Guid.NewGuid();

            // Act
            var actualReturnedTEntity = entityFrameworkRepository.Find(entityGuid);

            // Assert
            Assert.That(actualReturnedTEntity, Is.Null);
        }

        [Test]
        public void ReturnCorrectTEntityInstance_WhenRequestedGuidIsFound()
        {
            // Arrange
            var parserDbContext = new Mock<IParserDbContext>();

            var entities = new Mock<IDbSet<MockDbModel>>();
            parserDbContext.Setup(c => c.Set<MockDbModel>()).Returns(entities.Object);

            var entityFrameworkRepository = new EntityFrameworkRepository<MockDbModel>(parserDbContext.Object);

            var matchingGuid = Guid.NewGuid();

            var expectedReturnedTEntity = new MockDbModel() { Id = matchingGuid };

            var fakeData = new HashSet<MockDbModel>()
            {
                new MockDbModel() { Id = Guid.NewGuid() },
                new MockDbModel() { Id = Guid.NewGuid() },
                new MockDbModel() { Id = Guid.NewGuid() },
                new MockDbModel() { Id = Guid.NewGuid() },
                expectedReturnedTEntity,
                new MockDbModel() { Id = Guid.NewGuid() }
            };

            var fakeDataQueryable = fakeData.AsQueryable();

            entities.As<IQueryable>().Setup(e => e.GetEnumerator()).Returns(fakeDataQueryable.GetEnumerator());
            entities.As<IQueryable>().Setup(e => e.ElementType).Returns(fakeDataQueryable.ElementType);
            entities.As<IQueryable>().Setup(e => e.Expression).Returns(fakeDataQueryable.Expression);
            entities.As<IQueryable>().Setup(e => e.Provider).Returns(fakeDataQueryable.Provider);

            var entityGuid = matchingGuid;

            // Act
            var actualReturnedTEntity = entityFrameworkRepository.Find(entityGuid);

            // Assert
            Assert.That(actualReturnedTEntity, Is.SameAs(expectedReturnedTEntity));
        }
    }
}
