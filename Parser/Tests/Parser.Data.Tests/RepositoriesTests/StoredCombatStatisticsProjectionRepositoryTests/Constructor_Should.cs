using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.Repositories;

namespace Parser.Data.Tests.RepositoriesTests.StoredCombatStatisticsProjectionRepositoryTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectIStoredCombatStatisticsProjectionRepositoryInstance_WhenParametersAreCorrect()
        {
            // Arrange
            var storedCombatStatisticsRepository = new Mock<IStoredCombatStatisticsRepository>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            // Act
            var actualInstance = new StoredCombatStatisticsProjectionRepository(storedCombatStatisticsRepository.Object, objectMapperProvider.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<IStoredCombatStatisticsProjectionRepository>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIStoredCombatStatisticsRepositoryParameterIsNull()
        {
            // Arrange
            IStoredCombatStatisticsRepository storedCombatStatisticsRepository = null;
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            // Act & Assert
            Assert.That(
                () => new StoredCombatStatisticsProjectionRepository(storedCombatStatisticsRepository, objectMapperProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IStoredCombatStatisticsRepository)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIObjectMapperProviderParameterIsNull()
        {
            // Arrange
            var storedCombatStatisticsRepository = new Mock<IStoredCombatStatisticsRepository>();
            IObjectMapperProvider objectMapperProvider = null;

            // Act & Assert
            Assert.That(
                () => new StoredCombatStatisticsProjectionRepository(storedCombatStatisticsRepository.Object, objectMapperProvider),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IObjectMapperProvider)));
        }
    }
}
