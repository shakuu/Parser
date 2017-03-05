using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Engines;
using Parser.LogFileParser.Factories;

namespace Parser.LogFileParser.Tests.EnginesTests.LogFileParserEngineTests
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
            var exitCombatEventArgsFactory = new Mock<IExitCombatEventArgsFactory>();

            // Act
            var actualInstance = new LogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainer.Object, exitCombatEventArgsFactory.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<ILogFileParserEngine>());
            Assert.That(actualInstance, Is.InstanceOf<IExitCombatNotification>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenICommandResolutionHandlerParameterIsNull()
        {
            // Arrange
            ICommandResolutionHandler commandResolutionHandler = null;
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();
            var exitCombatEventArgsFactory = new Mock<IExitCombatEventArgsFactory>();

            // Act & Assert
            Assert.That(
                () => new LogFileParserEngine(commandResolutionHandler, combatStatisticsContainer.Object, exitCombatEventArgsFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICommandResolutionHandler)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenICombatStatisticsContainerParameterIsNull()
        {
            // Arrange
            var commandResolutionHandler = new Mock<ICommandResolutionHandler>();
            ICombatStatisticsContainer combatStatisticsContainer = null;
            var exitCombatEventArgsFactory = new Mock<IExitCombatEventArgsFactory>();

            // Act & Assert
            Assert.That(
                () => new LogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainer, exitCombatEventArgsFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICombatStatisticsContainer)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIExitCombatEventArgsFactoryParameterIsNull()
        {
            // Arrange
            var commandResolutionHandler = new Mock<ICommandResolutionHandler>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();
            IExitCombatEventArgsFactory exitCombatEventArgsFactory = null;

            // Act & Assert
            Assert.That(
                () => new LogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainer.Object, exitCombatEventArgsFactory),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IExitCombatEventArgsFactory)));
        }
    }
}
