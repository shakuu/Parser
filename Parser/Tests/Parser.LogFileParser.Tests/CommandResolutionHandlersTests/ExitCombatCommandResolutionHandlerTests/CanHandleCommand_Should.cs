using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.LogFileParser.Tests.Mocks;

namespace Parser.LogFileParser.Tests.CommandResolutionHandlersTests.ExitCombatCommandResolutionHandlerTests
{
    [TestFixture]
    public class CanHandleCommand_Should
    {
        [TestCase("")]
        [TestCase(null)]
        public void ReturnFalse_WhenICommandParameterEventNamePropertyIsANullOrEmptyString(string eventName)
        {
            // Arrange
            var enterCombatCommandResolutionHandler = new MockExitCombatCommandResolutionHandler();

            var command = new Mock<ICommand>();
            command.SetupGet(c => c.EventName).Returns(eventName);

            // Act 
            var actualResult = enterCombatCommandResolutionHandler.CanHandleCommand(command.Object);

            // Assert
            Assert.That(actualResult, Is.False);
        }

        [TestCase("random value")]
        [TestCase("abcdefghijklmnopqrstuvxyz")]
        [TestCase("EnterCombat")]
        [TestCase("exitCombat")]
        public void ReturnFalse_WhenICommandParameterEventNamePropertyIsDifferentComparedToEnterCombat(string eventName)
        {
            // Arrange
            var enterCombatCommandResolutionHandler = new MockExitCombatCommandResolutionHandler();

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
            var enterCombatCommandResolutionHandler = new MockExitCombatCommandResolutionHandler();

            var viableEventName = "ExitCombat";
            var command = new Mock<ICommand>();
            command.SetupGet(c => c.EventName).Returns(viableEventName);

            // Act 
            var actualResult = enterCombatCommandResolutionHandler.CanHandleCommand(command.Object);

            // Assert
            Assert.That(actualResult, Is.True);
        }
    }
}
