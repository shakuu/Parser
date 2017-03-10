using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.Factories;
using Parser.Data.Services.Strategies;
using Parser.Data.ViewModels;

namespace Parser.Data.Services.Tests.StrategiesTests.CombatStatisticsPersistentStorageStrategyTests
{
    [TestFixture]
    public class StoreCombatStatistics_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenIFinalizedCombatStatisticsParameterIsNull()
        {
            // Arrange
            var storedCombatStatisticsDataProvider = new Mock<IStoredCombatStatisticsDataProvider>();
            var businessTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var combatStatisticsPersistentStorageStrategy = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsDataProvider.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

            IFinalizedCombatStatistics finalizedCombatStatistics = null;

            // Act & Assert
            Assert.That(
                () => combatStatisticsPersistentStorageStrategy.StoreCombatStatistics(finalizedCombatStatistics),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IFinalizedCombatStatistics)));
        }

        [Test]
        public void InvokeIObjectMapperProvider_MapMethodOnceWithCorrectParameters()
        {
            // Arrange
            var storedCombatStatisticsDataProvider = new Mock<IStoredCombatStatisticsDataProvider>();
            var businessTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var businessTransaction = new Mock<IEntityFrameworkTransaction>();
            businessTransactionFactory.Setup(f => f.CreateEntityFrameworkTransaction()).Returns(businessTransaction.Object);

            var combatStatisticsPersistentStorageStrategy = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsDataProvider.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

            var finalizedCombatStatistics = new Mock<IFinalizedCombatStatistics>();

            // Act
            combatStatisticsPersistentStorageStrategy.StoreCombatStatistics(finalizedCombatStatistics.Object);

            // Assert
            objectMapperProvider.Verify(p => p.Map<StoredCombatStatisticsViewModel>(finalizedCombatStatistics.Object), Times.Once);
        }

        [Test]
        public void InvokeIBusinessTransactionFactory_CreateBusinessTransactionOnce()
        {
            // Arrange
            var storedCombatStatisticsDataProvider = new Mock<IStoredCombatStatisticsDataProvider>();
            var businessTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var businessTransaction = new Mock<IEntityFrameworkTransaction>();
            businessTransactionFactory.Setup(f => f.CreateEntityFrameworkTransaction()).Returns(businessTransaction.Object);

            var combatStatisticsPersistentStorageStrategy = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsDataProvider.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

            var finalizedCombatStatistics = new Mock<IFinalizedCombatStatistics>();

            // Act
            combatStatisticsPersistentStorageStrategy.StoreCombatStatistics(finalizedCombatStatistics.Object);

            // Assert
            businessTransactionFactory.Verify(f => f.CreateEntityFrameworkTransaction(), Times.Once);
        }

        [Test]
        public void InvokeIStoredCombatStatisticsDataProvider_CreateOnceWithCorrectParameter()
        {
            // Arrange
            var storedCombatStatisticsDataProvider = new Mock<IStoredCombatStatisticsDataProvider>();
            var businessTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var businessTransaction = new Mock<IEntityFrameworkTransaction>();
            businessTransactionFactory.Setup(f => f.CreateEntityFrameworkTransaction()).Returns(businessTransaction.Object);

            var storedCombatStatisticsProjection = new StoredCombatStatisticsViewModel();
            objectMapperProvider.Setup(p => p.Map<StoredCombatStatisticsViewModel>(It.IsAny<IFinalizedCombatStatistics>())).Returns(storedCombatStatisticsProjection);

            var combatStatisticsPersistentStorageStrategy = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsDataProvider.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

            var finalizedCombatStatistics = new Mock<IFinalizedCombatStatistics>();

            // Act
            combatStatisticsPersistentStorageStrategy.StoreCombatStatistics(finalizedCombatStatistics.Object);

            // Assert
            storedCombatStatisticsDataProvider.Verify(r => r.CreateStoredCombatStatistics(storedCombatStatisticsProjection), Times.Once);
        }

        [Test]
        public void InvokeIBusinessTransaction_CommitOnce()
        {
            // Arrange
            var storedCombatStatisticsDataProvider = new Mock<IStoredCombatStatisticsDataProvider>();
            var businessTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var businessTransaction = new Mock<IEntityFrameworkTransaction>();
            businessTransactionFactory.Setup(f => f.CreateEntityFrameworkTransaction()).Returns(businessTransaction.Object);

            var storedCombatStatisticsProjection = new StoredCombatStatisticsViewModel();
            objectMapperProvider.Setup(p => p.Map<StoredCombatStatisticsViewModel>(It.IsAny<IFinalizedCombatStatistics>())).Returns(storedCombatStatisticsProjection);

            var combatStatisticsPersistentStorageStrategy = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsDataProvider.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

            var finalizedCombatStatistics = new Mock<IFinalizedCombatStatistics>();

            // Act
            combatStatisticsPersistentStorageStrategy.StoreCombatStatistics(finalizedCombatStatistics.Object);

            // Assert
            businessTransaction.Verify(bt => bt.SaveChanges(), Times.Once);
        }

        [Test]
        public void ReturnCorrectIFinalizedCombatStatisticsInstance()
        {
            // Arrange
            var storedCombatStatisticsDataProvider = new Mock<IStoredCombatStatisticsDataProvider>();
            var businessTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var businessTransaction = new Mock<IEntityFrameworkTransaction>();
            businessTransactionFactory.Setup(f => f.CreateEntityFrameworkTransaction()).Returns(businessTransaction.Object);

            var storedCombatStatisticsProjection = new StoredCombatStatisticsViewModel();
            objectMapperProvider.Setup(p => p.Map<StoredCombatStatisticsViewModel>(It.IsAny<IFinalizedCombatStatistics>())).Returns(storedCombatStatisticsProjection);

            var combatStatisticsPersistentStorageStrategy = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsDataProvider.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

            var finalizedCombatStatistics = new Mock<IFinalizedCombatStatistics>();
            var expectedReturnedIFinalizedCombatStatisticsInstance = finalizedCombatStatistics.Object;
            // Act
            var actualReturnedIFinalizedCombatStatisticsInstance = combatStatisticsPersistentStorageStrategy.StoreCombatStatistics(finalizedCombatStatistics.Object);

            // Assert
            Assert.That(actualReturnedIFinalizedCombatStatisticsInstance, Is.SameAs(expectedReturnedIFinalizedCombatStatisticsInstance));
        }
    }
}
