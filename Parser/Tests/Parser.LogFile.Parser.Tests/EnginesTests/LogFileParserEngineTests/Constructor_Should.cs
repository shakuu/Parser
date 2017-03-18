using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.EventsArgs;
using Parser.LogFile.Parser.Contracts;
using Parser.LogFile.Parser.Engines;

namespace Parser.LogFile.Parser.Tests.EnginesTests.LogFileParserEngineTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectILogFileParserEngineInstance_WhenParametersAreValid()
        {
            // Arrange
            var commandResolutionHandler = new Mock<ICommandResolutionHandler>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();
            var combatStatisticsFinalizationStrategy = new Mock<ICombatStatisticsFinalizationStrategy>();
            var combatStatisticsPersistentStorageStrategy = new Mock<ICombatStatisticsPersistentStorageStrategy>();

            var currentCombatStatisticsChangedSubscribeProvider = new Mock<ICurrentCombatStatisticsChangedSubscribeProvider>();
            combatStatisticsContainer.SetupGet(c => c.OnCurrentCombatStatisticsChanged).Returns(currentCombatStatisticsChangedSubscribeProvider.Object);

            // Act
            var actualInstance = new LogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainer.Object, combatStatisticsFinalizationStrategy.Object, combatStatisticsPersistentStorageStrategy.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<ILogFileParserEngine>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenICommandResolutionHandlerParameterIsNull()
        {
            // Arrange
            ICommandResolutionHandler commandResolutionHandler = null;
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();
            var combatStatisticsFinalizationStrategy = new Mock<ICombatStatisticsFinalizationStrategy>();
            var combatStatisticsPersistentStorageStrategy = new Mock<ICombatStatisticsPersistentStorageStrategy>();

            // Act & Assert
            Assert.That(
                () => new LogFileParserEngine(commandResolutionHandler, combatStatisticsContainer.Object, combatStatisticsFinalizationStrategy.Object, combatStatisticsPersistentStorageStrategy.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICommandResolutionHandler)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenICombatStatisticsContainerParameterIsNull()
        {
            // Arrange
            var commandResolutionHandler = new Mock<ICommandResolutionHandler>();
            ICombatStatisticsContainer combatStatisticsContainer = null;
            var combatStatisticsFinalizationStrategy = new Mock<ICombatStatisticsFinalizationStrategy>();
            var combatStatisticsPersistentStorageStrategy = new Mock<ICombatStatisticsPersistentStorageStrategy>();

            // Act & Assert
            Assert.That(
                () => new LogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainer, combatStatisticsFinalizationStrategy.Object, combatStatisticsPersistentStorageStrategy.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICombatStatisticsContainer)));
        }

        [Test]
        public void InvokeICombatStatisticsContainerParameter_OnCurrentCombatStatisticsChangedPropertySubscribeMethod()
        {
            // Arrange
            var commandResolutionHandler = new Mock<ICommandResolutionHandler>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();
            var combatStatisticsFinalizationStrategy = new Mock<ICombatStatisticsFinalizationStrategy>();
            var combatStatisticsPersistentStorageStrategy = new Mock<ICombatStatisticsPersistentStorageStrategy>();

            var currentCombatStatisticsChangedEventHandlerProvider = new Mock<ICurrentCombatStatisticsChangedEventHandlerProvider>();
            combatStatisticsContainer.SetupGet(c => c.OnCurrentCombatStatisticsChanged).Returns(currentCombatStatisticsChangedEventHandlerProvider.Object);

            // Act
            var actualInstance = new LogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainer.Object, combatStatisticsFinalizationStrategy.Object, combatStatisticsPersistentStorageStrategy.Object);

            // Assert
            currentCombatStatisticsChangedEventHandlerProvider.Verify(p => p.Subscribe(It.IsAny<EventHandler<CurrentCombatStatisticsChangedEventArgs>>()), Times.Once);
        }
    }
}
