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
            var storedCombatStatisticsDataProvider = new Mock<IStoredCombatStatisticsDataProvider>();
            var businessTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            // Act
            var actualInstance = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsDataProvider.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<ICombatStatisticsPersistentStorageStrategy>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIStoredCombatStatisticsDataProviderParameterIsNull()
        {
            // Arrange
            IStoredCombatStatisticsDataProvider storedCombatStatisticsDataProvider = null;
            var businessTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            // Act & Assert
            Assert.That(
                () => new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsDataProvider, businessTransactionFactory.Object, objectMapperProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IStoredCombatStatisticsDataProvider)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIBusinessTransactionFactoryParameterIsNull()
        {
            // Arrange
            var storedCombatStatisticsDataProvider = new Mock<IStoredCombatStatisticsDataProvider>();
            IEntityFrameworkTransactionFactory businessTransactionFactory = null;
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            // Act & Assert
            Assert.That(
                () => new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsDataProvider.Object, businessTransactionFactory, objectMapperProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IEntityFrameworkTransactionFactory)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIObjectMapperProviderParameterIsNull()
        {
            // Arrange
            var storedCombatStatisticsDataProvider = new Mock<IStoredCombatStatisticsDataProvider>();
            var businessTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();
            IObjectMapperProvider objectMapperProvider = null;

            // Act & Assert
            Assert.That(
                () => new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsDataProvider.Object, businessTransactionFactory.Object, objectMapperProvider),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IObjectMapperProvider)));
        }
    }
}
