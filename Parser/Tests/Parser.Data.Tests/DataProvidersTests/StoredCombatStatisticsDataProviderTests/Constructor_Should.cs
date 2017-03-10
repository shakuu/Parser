using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.DataProviders;
using Parser.Data.Models;

namespace Parser.Data.Tests.DataProvidersTests.StoredCombatStatisticsDataProviderTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectIStoredCombatStatisticsDataProviderInstance_WhenParametersAreCorrect()
        {
            // Arrange
            var storedCombatStatisticsEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<StoredCombatStatistics>>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            // Act
            var actualInstance = new StoredCombatStatisticsDataProvider(storedCombatStatisticsEntityFrameworkRepository.Object, objectMapperProvider.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<IStoredCombatStatisticsDataProvider>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIEntityFrameworkRepositoryParametersIsNull()
        {
            // Arrange
            IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository = null;
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            // Act & Assert
            Assert.That(
                () => new StoredCombatStatisticsDataProvider(storedCombatStatisticsEntityFrameworkRepository, objectMapperProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IEntityFrameworkRepository<StoredCombatStatistics>)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIObjectMapperProviderParametersIsNull()
        {
            // Arrange
            var storedCombatStatisticsEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<StoredCombatStatistics>>();
            IObjectMapperProvider objectMapperProvider = null;

            // Act & Assert
            Assert.That(
                () => new StoredCombatStatisticsDataProvider(storedCombatStatisticsEntityFrameworkRepository.Object, objectMapperProvider),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IObjectMapperProvider)));
        }
    }
}
