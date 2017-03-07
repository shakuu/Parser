using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.Factories;
using Parser.Data.Services.Strategies;
using Parser.Data.Projections;

namespace Parser.Data.Services.Tests.StrategiesTests.CombatStatisticsPersistentStorageStrategyTests
{
    [TestFixture]
    public class StoreCombatStatistics_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenIFinalizedCombatStatisticsParameterIsNull()
        {
            // Arrange
            var storedCombatStatisticsProjectionRepository = new Mock<IStoredCombatStatisticsProjectionRepository>();
            var businessTransactionFactory = new Mock<IBusinessTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var combatStatisticsPersistentStorageStrategy = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsProjectionRepository.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

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
            var storedCombatStatisticsProjectionRepository = new Mock<IStoredCombatStatisticsProjectionRepository>();
            var businessTransactionFactory = new Mock<IBusinessTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var businessTransaction = new Mock<IBusinessTransaction>();
            businessTransactionFactory.Setup(f => f.CreateBusinessTransaction()).Returns(businessTransaction.Object);

            var combatStatisticsPersistentStorageStrategy = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsProjectionRepository.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

            var finalizedCombatStatistics = new Mock<IFinalizedCombatStatistics>();

            // Act
            combatStatisticsPersistentStorageStrategy.StoreCombatStatistics(finalizedCombatStatistics.Object);

            // Assert
            objectMapperProvider.Verify(p => p.Map<StoredCombatStatisticsProjection>(finalizedCombatStatistics.Object), Times.Once);
        }

        [Test]
        public void InvokeIBusinessTransactionFactory_CreateBusinessTransactionOnce()
        {
            // Arrange
            var storedCombatStatisticsProjectionRepository = new Mock<IStoredCombatStatisticsProjectionRepository>();
            var businessTransactionFactory = new Mock<IBusinessTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var businessTransaction = new Mock<IBusinessTransaction>();
            businessTransactionFactory.Setup(f => f.CreateBusinessTransaction()).Returns(businessTransaction.Object);

            var combatStatisticsPersistentStorageStrategy = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsProjectionRepository.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

            var finalizedCombatStatistics = new Mock<IFinalizedCombatStatistics>();

            // Act
            combatStatisticsPersistentStorageStrategy.StoreCombatStatistics(finalizedCombatStatistics.Object);

            // Assert
            businessTransactionFactory.Verify(f => f.CreateBusinessTransaction(), Times.Once);
        }

        [Test]
        public void InvokeIStoredCombatStatisticsProjectionRepository_CreateOnceWithCorrectParameter()
        {
            // Arrange
            var storedCombatStatisticsProjectionRepository = new Mock<IStoredCombatStatisticsProjectionRepository>();
            var businessTransactionFactory = new Mock<IBusinessTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var businessTransaction = new Mock<IBusinessTransaction>();
            businessTransactionFactory.Setup(f => f.CreateBusinessTransaction()).Returns(businessTransaction.Object);

            var storedCombatStatisticsProjection = new StoredCombatStatisticsProjection();
            objectMapperProvider.Setup(p => p.Map<StoredCombatStatisticsProjection>(It.IsAny<IFinalizedCombatStatistics>())).Returns(storedCombatStatisticsProjection);

            var combatStatisticsPersistentStorageStrategy = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsProjectionRepository.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

            var finalizedCombatStatistics = new Mock<IFinalizedCombatStatistics>();

            // Act
            combatStatisticsPersistentStorageStrategy.StoreCombatStatistics(finalizedCombatStatistics.Object);

            // Assert
            storedCombatStatisticsProjectionRepository.Verify(r => r.Create(storedCombatStatisticsProjection), Times.Once);
        }

        [Test]
        public void InvokeIBusinessTransaction_CommitOnce()
        {
            // Arrange
            var storedCombatStatisticsProjectionRepository = new Mock<IStoredCombatStatisticsProjectionRepository>();
            var businessTransactionFactory = new Mock<IBusinessTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var businessTransaction = new Mock<IBusinessTransaction>();
            businessTransactionFactory.Setup(f => f.CreateBusinessTransaction()).Returns(businessTransaction.Object);

            var storedCombatStatisticsProjection = new StoredCombatStatisticsProjection();
            objectMapperProvider.Setup(p => p.Map<StoredCombatStatisticsProjection>(It.IsAny<IFinalizedCombatStatistics>())).Returns(storedCombatStatisticsProjection);

            var combatStatisticsPersistentStorageStrategy = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsProjectionRepository.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

            var finalizedCombatStatistics = new Mock<IFinalizedCombatStatistics>();

            // Act
            combatStatisticsPersistentStorageStrategy.StoreCombatStatistics(finalizedCombatStatistics.Object);

            // Assert
            businessTransaction.Verify(bt => bt.Commit(), Times.Once);
        }

        [Test]
        public void ReturnCorrectIFinalizedCombatStatisticsInstance()
        {
            // Arrange
            var storedCombatStatisticsProjectionRepository = new Mock<IStoredCombatStatisticsProjectionRepository>();
            var businessTransactionFactory = new Mock<IBusinessTransactionFactory>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var businessTransaction = new Mock<IBusinessTransaction>();
            businessTransactionFactory.Setup(f => f.CreateBusinessTransaction()).Returns(businessTransaction.Object);

            var storedCombatStatisticsProjection = new StoredCombatStatisticsProjection();
            objectMapperProvider.Setup(p => p.Map<StoredCombatStatisticsProjection>(It.IsAny<IFinalizedCombatStatistics>())).Returns(storedCombatStatisticsProjection);

            var combatStatisticsPersistentStorageStrategy = new CombatStatisticsPersistentStorageStrategy(storedCombatStatisticsProjectionRepository.Object, businessTransactionFactory.Object, objectMapperProvider.Object);

            var finalizedCombatStatistics = new Mock<IFinalizedCombatStatistics>();
            var expectedReturnedIFinalizedCombatStatisticsInstance = finalizedCombatStatistics.Object;
            // Act
            var actualReturnedIFinalizedCombatStatisticsInstance = combatStatisticsPersistentStorageStrategy.StoreCombatStatistics(finalizedCombatStatistics.Object);

            // Assert
            Assert.That(actualReturnedIFinalizedCombatStatisticsInstance, Is.SameAs(expectedReturnedIFinalizedCombatStatisticsInstance));
        }
    }
}
