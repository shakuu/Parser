using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Factories;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Engines;

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
            var combatStatisticsContainerFactory = new Mock<ICombatStatisticsContainerFactory>();

            // Act
            var actualInstance = new LogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainerFactory.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null.And.InstanceOf<ICombatStatisticsContainerFactory>());
        }

        [Test]
        public void InvokeICombatStatisticsContainerFactory_CreateCombatStatisticsContainerMethodOnce()
        {
            // Arrange
            var commandResolutionHandler = new Mock<ICommandResolutionHandler>();
            var combatStatisticsContainerFactory = new Mock<ICombatStatisticsContainerFactory>();

            // Act
            var logFileParserEngine = new LogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainerFactory.Object);

            // Assert
            combatStatisticsContainerFactory.Verify(f => f.CreateCombatStatisticsContainer(), Times.Once);
        }

        [Test]
        public void ThrowArgumentNullException_WhenICommandResolutionHandlerParameterIsNull()
        {
            // Arrange
            ICommandResolutionHandler commandResolutionHandler = null;
            var combatStatisticsContainerFactory = new Mock<ICombatStatisticsContainerFactory>();

            // Act & Assert
            Assert.That(
                () => new LogFileParserEngine(commandResolutionHandler, combatStatisticsContainerFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICommandResolutionHandler)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenICombatStatisticsContainerFactoryParameterIsNull()
        {
            // Arrange
            var commandResolutionHandler = new Mock<ICommandResolutionHandler>();
            ICombatStatisticsContainerFactory combatStatisticsContainerFactory = null;

            // Act & Assert
            Assert.That(
                () => new LogFileParserEngine(commandResolutionHandler.Object, combatStatisticsContainerFactory),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICombatStatisticsContainerFactory)));
        }
    }
}
