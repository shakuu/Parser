using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFileParser.Tests.Mocks;

namespace Parser.LogFileParser.Tests.CommandResolutionHandlersTests.BaseTests.CommandResolutionHandlerTests
{
    [TestFixture]
    public class CanHandleCommand_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenICommandParameterIsNull()
        {
            // Arrange
            var matchingEventName = "MockEvent";

            var commandResolutionHandler = new MockCommandResolutionHandler(matchingEventName);

            ICommand command = null;

            // Act & Assert
            Assert.That(
                () => commandResolutionHandler.CanHandleCommand(command),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICommand)));
        }

        [TestCase("")]
        [TestCase(null)]
        public void ReturnFalse_WhenICommandParameterEventNamePropertyIsANullOrEmptyString(string eventName)
        {
            // Arrange
            var matchingEventName = "MockEvent";

            var commandResolutionHandler = new MockCommandResolutionHandler(matchingEventName);

            var command = new Mock<ICommand>();
            command.SetupGet(c => c.EventName).Returns(eventName);

            // Act 
            var actualResult = commandResolutionHandler.CanHandleCommand(command.Object);

            // Assert
            Assert.That(actualResult, Is.False);
        }

        [TestCase("random value")]
        [TestCase("abcdefghijklmnopqrstuvxyz")]
        [TestCase("ExitCombat")]
        [TestCase("enterCombat")]
        [TestCase("mockEvent")]
        public void ReturnFalse_WhenICommandParameterEventNamePropertyIsDifferentComparedToEnterCombat(string eventName)
        {
            // Arrange
            var matchingEventName = "MockEvent";

            var commandResolutionHandler = new MockCommandResolutionHandler(matchingEventName);

            var command = new Mock<ICommand>();
            command.SetupGet(c => c.EventName).Returns(eventName);

            // Act 
            var actualResult = commandResolutionHandler.CanHandleCommand(command.Object);

            // Assert
            Assert.That(actualResult, Is.False);
        }

        [Test]
        public void ReturnTrue_WhenICommandParameterEventNamePropertyIsIdenticalToEnterCombat()
        {
            // Arrange
            var matchingEventName = "MockEvent";

            var commandResolutionHandler = new MockCommandResolutionHandler(matchingEventName);

            var viableEventName = "MockEvent";
            var command = new Mock<ICommand>();
            command.SetupGet(c => c.EventName).Returns(viableEventName);

            // Act 
            var actualResult = commandResolutionHandler.CanHandleCommand(command.Object);

            // Assert
            Assert.That(actualResult, Is.True);
        }
    }
}
