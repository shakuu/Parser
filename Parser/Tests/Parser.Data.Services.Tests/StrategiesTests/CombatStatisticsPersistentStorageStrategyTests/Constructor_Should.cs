using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.Factories;
using Parser.Data.Services.Strategies;
using Parser.LogFileParser.Contracts;

namespace Parser.Data.Services.Tests.StrategiesTests.CombatStatisticsPersistentStorageStrategyTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectICombatStatisticsPersistentStorageStrategyInstance_WhenParametersAreValid()
        {
            // Arrange
            var storedCombatStatisticsProjectionRepository = new Mock<IStoredCombatStatisticsProjectionRepository>();
            var businessTransactionFactory = new Mock<IBusinessTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            // Act
            var actualInstance = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsProjectionRepository.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<ICombatStatisticsPersistentStorageStrategy>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIStoredCombatStatisticsProjectionRepositoryParameterIsNull()
        {
            // Arrange
            IStoredCombatStatisticsProjectionRepository storedCombatStatisticsProjectionRepository = null;
            var businessTransactionFactory = new Mock<IBusinessTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            // Act & Assert
            Assert.That(
                () => new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsProjectionRepository, businessTransactionFactory.Object, objectMapperProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IStoredCombatStatisticsProjectionRepository)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIBusinessTransactionFactoryParameterIsNull()
        {
            // Arrange
            var storedCombatStatisticsProjectionRepository = new Mock<IStoredCombatStatisticsProjectionRepository>();
            IBusinessTransactionFactory businessTransactionFactory = null;
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            // Act & Assert
            Assert.That(
                () => new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsProjectionRepository.Object, businessTransactionFactory, objectMapperProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IBusinessTransactionFactory)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIObjectMapperProviderParameterIsNull()
        {
            // Arrange
            var storedCombatStatisticsProjectionRepository = new Mock<IStoredCombatStatisticsProjectionRepository>();
            var businessTransactionFactory = new Mock<IBusinessTransactionFactory>();
            IObjectMapperProvider objectMapperProvider = null;

            // Act & Assert
            Assert.That(
                () => new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsProjectionRepository.Object, businessTransactionFactory.Object, objectMapperProvider),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IObjectMapperProvider)));
        }
    }
}
