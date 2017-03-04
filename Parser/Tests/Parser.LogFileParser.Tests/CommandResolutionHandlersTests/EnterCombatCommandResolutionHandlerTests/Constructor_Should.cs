using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Factories;
using Parser.LogFileParser.CommandResolutionHandlers;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.Tests.CommandResolutionHandlersTests.EnterCombatCommandResolutionHandlerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectICommandResolutionHandlerInstance_WhenParametersAreValid()
        {
            // Arrange
            var combatStatisticsFactory = new Mock<ICombatStatisticsFactory>();

            // Act
            var actualInstance = new EnterCombatCommandResolutionHandler(combatStatisticsFactory.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<ICommandResolutionHandler>());
            Assert.That(actualInstance, Is.InstanceOf<ICommandResolutionHandlerChain>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenICombatStatisticsFactoryParametersIsNull()
        {
            // Arrange
            ICombatStatisticsFactory combatStatisticsFactory = null;

            // Act & Assert
            Assert.That(
                () => new EnterCombatCommandResolutionHandler(combatStatisticsFactory),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICombatStatisticsFactory)));
        }
    }
}
