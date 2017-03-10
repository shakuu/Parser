using System.Data.Entity;

using Moq;
using NUnit.Framework;

using Parser.Data.Contracts;
using Parser.Data.Repositories;
using Parser.Data.Tests.Mocks;

namespace Parser.Data.Tests.RepositoriesTests.EntityFrameworkRepositoryTests
{
    [TestFixture]
    public class EntitiesGet_Should
    {
        [Test]
        public void ReturnCorrectIQueryableTEntityInstance()
        {
            // Arrange
            var parserDbContext = new Mock<IParserDbContext>();

            var entities = new Mock<IDbSet<MockDbModel>>();
            parserDbContext.Setup(c => c.Set<MockDbModel>()).Returns(entities.Object);

            var entityFrameworkRepository = new EntityFrameworkRepository<MockDbModel>(parserDbContext.Object);

            var expectedReturnerIQueryableTEntityInstance = entities.Object;

            // Act
            var actualReturnerIQueryableTEntityInstance = entityFrameworkRepository.Entities;

            // Assert
            Assert.That(actualReturnerIQueryableTEntityInstance, Is.SameAs(expectedReturnerIQueryableTEntityInstance));
        }
    }
}
