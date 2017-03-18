using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.EventsArgs;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Tests.Mocks;

namespace Parser.LogFileParser.Tests.EnginesTests.LogFileParserEngineTests
{
    [TestFixture]
    public class OnCurrentCombatStatisticsChanged_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenCurrentCombatStatisticsChangedEventArgsParameterIsNull()
        {
            // Arrange
            var commandResolutionHandler = new Mock<ICommandResolutionHandler>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();
            var combatStatisticsFinalizationStrategy = new Mock<ICombatStatisticsFinalizationStrategy>();
            var combatStatisticsPersistentStorageStrategy = new Mock<ICombatStatisticsPersistentStorageStrategy>();

            var currentCombatStatisticsChangedSubscribeProvider = new Mock<ICurrentCombatStatisticsChangedSubscribeProvider>();
            combatStatisticsContainer.SetupGet(c => c.OnCurrentCombatStatisticsChanged).Returns(currentCombatStatisticsChangedSubscribeProvider.Object);

            var logFileParserEngine = new MockLogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainer.Object, combatStatisticsFinalizationStrategy.Object, combatStatisticsPersistentStorageStrategy.Object);

            CurrentCombatStatisticsChangedEventArgs args = null;

            // Act & Assert
            Assert.That(
                () => logFileParserEngine.OnCurrentCombatStatisticsChanged(null, args),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(CurrentCombatStatisticsChangedEventArgs)));
        }

        [Test]
        public void InvokeICombatStatisticsFinalizationStrategy_FinalizeCombatStatisticsOnceWithCorrectParameter()
        {
            // Arrange
            var commandResolutionHandler = new Mock<ICommandResolutionHandler>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();
            var combatStatisticsFinalizationStrategy = new Mock<ICombatStatisticsFinalizationStrategy>();
            var combatStatisticsPersistentStorageStrategy = new Mock<ICombatStatisticsPersistentStorageStrategy>();

            var currentCombatStatisticsChangedSubscribeProvider = new Mock<ICurrentCombatStatisticsChangedSubscribeProvider>();
            combatStatisticsContainer.SetupGet(c => c.OnCurrentCombatStatisticsChanged).Returns(currentCombatStatisticsChangedSubscribeProvider.Object);

            var logFileParserEngine = new MockLogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainer.Object, combatStatisticsFinalizationStrategy.Object, combatStatisticsPersistentStorageStrategy.Object);

            var combatStatistics = new Mock<ICombatStatistics>();
            var args = new CurrentCombatStatisticsChangedEventArgs(combatStatistics.Object);

            // Act
            logFileParserEngine.OnCurrentCombatStatisticsChanged(null, args);

            // Assert
            combatStatisticsFinalizationStrategy.Verify(s => s.FinalizeCombatStatistics(args.CombatStatistics), Times.Once);
        }

        [Test]
        public void InvokeICombatStatisticsPersistentStorageStrategy_StoreCombatStatisticsOnceWithCorrectParameter()
        {
            // Arrange
            var commandResolutionHandler = new Mock<ICommandResolutionHandler>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();
            var combatStatisticsFinalizationStrategy = new Mock<ICombatStatisticsFinalizationStrategy>();
            var combatStatisticsPersistentStorageStrategy = new Mock<ICombatStatisticsPersistentStorageStrategy>();

            var currentCombatStatisticsChangedSubscribeProvider = new Mock<ICurrentCombatStatisticsChangedSubscribeProvider>();
            combatStatisticsContainer.SetupGet(c => c.OnCurrentCombatStatisticsChanged).Returns(currentCombatStatisticsChangedSubscribeProvider.Object);

            var logFileParserEngine = new MockLogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainer.Object, combatStatisticsFinalizationStrategy.Object, combatStatisticsPersistentStorageStrategy.Object);

            var combatStatistics = new Mock<ICombatStatistics>();
            var args = new CurrentCombatStatisticsChangedEventArgs(combatStatistics.Object);

            var finalizedCombatStatistics = new Mock<IFinalizedCombatStatistics>();
            combatStatisticsFinalizationStrategy.Setup(s => s.FinalizeCombatStatistics(It.IsAny<ICombatStatistics>())).Returns(finalizedCombatStatistics.Object);

            // Act
            logFileParserEngine.OnCurrentCombatStatisticsChanged(null, args);

            // Assert
            combatStatisticsPersistentStorageStrategy.Verify(s => s.StoreCombatStatistics(finalizedCombatStatistics.Object), Times.Once);
        }
    }
}
