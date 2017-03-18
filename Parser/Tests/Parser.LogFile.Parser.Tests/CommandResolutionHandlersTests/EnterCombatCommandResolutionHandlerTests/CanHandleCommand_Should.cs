using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFile.Parser.Tests.Mocks;

namespace Parser.LogFile.Parser.Tests.CommandResolutionHandlersTests.EnterCombatCommandResolutionHandlerTests
{
    [TestFixture]
    public class CanHandleCommand_Should
    {
        [TestCase("")]
        [TestCase(null)]
        public void ReturnFalse_WhenICommandParameterEventNamePropertyIsANullOrEmptyString(string eventName)
        {
            // Arrange
            var combatStatisticsFactory = new Mock<ICombatStatisticsFactory>();

            var enterCombatCommandResolutionHandler = new MockEnterCombatCommandResolutionHandler(combatStatisticsFactory.Object);

            var command = new Mock<ICommand>();
            command.SetupGet(c => c.EventName).Returns(eventName);

            // Act 
            var actualResult = enterCombatCommandResolutionHandler.CanHandleCommand(command.Object);

            // Assert
            Assert.That(actualResult, Is.False);
        }

        [TestCase("random value")]
        [TestCase("abcdefghijklmnopqrstuvxyz")]
        [TestCase("ExitCombat")]
        [TestCase("enterCombat")]
        public void ReturnFalse_WhenICommandParameterEventNamePropertyIsDifferentComparedToEnterCombat(string eventName)
        {
            // Arrange
            var combatStatisticsFactory = new Mock<ICombatStatisticsFactory>();

            var enterCombatCommandResolutionHandler = new MockEnterCombatCommandResolutionHandler(combatStatisticsFactory.Object);

            var command = new Mock<ICommand>();
            command.SetupGet(c => c.EventName).Returns(eventName);

            // Act 
            var actualResult = enterCombatCommandResolutionHandler.CanHandleCommand(command.Object);

            // Assert
            Assert.That(actualResult, Is.False);
        }

        [Test]
        public void ReturnTrue_WhenICommandParameterEventNamePropertyIsIdenticalToEnterCombat()
        {
            // Arrange
            var combatStatisticsFactory = new Mock<ICombatStatisticsFactory>();

            var enterCombatCommandResolutionHandler = new MockEnterCombatCommandResolutionHandler(combatStatisticsFactory.Object);

            var viableEventName = "EnterCombat";
            var command = new Mock<ICommand>();
            command.SetupGet(c => c.EventName).Returns(viableEventName);

            // Act 
            var actualResult = enterCombatCommandResolutionHandler.CanHandleCommand(command.Object);

            // Assert
            Assert.That(actualResult, Is.True);
        }
    }
}
