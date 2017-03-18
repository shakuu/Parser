using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Tests.Mocks;

namespace Parser.LogFileParser.Tests.CommandResolutionHandlersTests.BaseTests.CommandResolutionHandlerTests
{
    [TestFixture]
    public class ResolveCommand_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenICommandParameterIsNull()
        {
            // Arrange
            var commandResolutionHandler = new OverriddenCanHandleCommandMethodMockCommandResolutionHandler();

            ICommand command = null;
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            // Act & Assert
            Assert.That(
                () => commandResolutionHandler.ResolveCommand(command, combatStatisticsContainer.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICommand)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenICombatStatisticsContainerParameterIsNull()
        {
            // Arrange
            var commandResolutionHandler = new OverriddenCanHandleCommandMethodMockCommandResolutionHandler();

            var command = new Mock<ICommand>();
            ICombatStatisticsContainer combatStatisticsContainer = null;

            // Act & Assert
            Assert.That(
                () => commandResolutionHandler.ResolveCommand(command.Object, combatStatisticsContainer),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICombatStatisticsContainer)));
        }

        [Test]
        public void InvokeCanHandleCommandMethod()
        {
            // Arrange
            var commandResolutionHandler = new OverriddenCanHandleCommandMethodMockCommandResolutionHandler();

            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            // Act
            commandResolutionHandler.ResolveCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            Assert.That(commandResolutionHandler.CanHandleCommandMethodIsCalled, Is.True);
        }

        [Test]
        public void InvokeCanHandleCommandMethod_WithCorrectParameter()
        {
            // Arrange
            var commandResolutionHandler = new OverriddenCanHandleCommandMethodMockCommandResolutionHandler();

            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            // Act
            commandResolutionHandler.ResolveCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            Assert.That(commandResolutionHandler.CanHandleCommandMethodICommandParameter, Is.SameAs(command.Object));
        }

        [Test]
        public void InvokeHandleCommandMethod_WhenCanHandleMethodReturnsTrue()
        {
            // Arrange
            var commandResolutionHandler = new OverriddenCanHandleCommandMethodMockCommandResolutionHandler();

            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            commandResolutionHandler.CanHandleCommandMethodReturnValue = true;

            // Act
            commandResolutionHandler.ResolveCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            Assert.That(commandResolutionHandler.HandleCommandMethodIsCalled, Is.True);
        }

        [Test]
        public void InvokeHandleCommandMethodWithCorrectParameters_WhenCanHandleMethodReturnsTrue()
        {
            // Arrange
            var commandResolutionHandler = new OverriddenCanHandleCommandMethodMockCommandResolutionHandler();

            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            commandResolutionHandler.CanHandleCommandMethodReturnValue = true;

            // Act
            commandResolutionHandler.ResolveCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            Assert.That(commandResolutionHandler.HandleCommandMethodICommandParameter, Is.SameAs(command.Object));
            Assert.That(commandResolutionHandler.HandleCommandMethodICombatStatisticsContainerParameter, Is.SameAs(combatStatisticsContainer.Object));
        }

        [Test]
        public void InvokeNextCommandResolutionHandler_ResolveCommandMethodWithCorrectParameters_WhenCanHandleMethodReturnsFalse()
        {
            // Arrange
            var commandResolutionHandler = new OverriddenCanHandleCommandMethodMockCommandResolutionHandler();

            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            commandResolutionHandler.CanHandleCommandMethodReturnValue = false;

            var nextCommandResolutionHandler = new Mock<ICommandResolutionHandler>();
            commandResolutionHandler.NextCommandResolutionHandler = nextCommandResolutionHandler.Object;

            // Act
            commandResolutionHandler.ResolveCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            nextCommandResolutionHandler.Verify(h => h.ResolveCommand(command.Object, combatStatisticsContainer.Object), Times.Once);
        }

        [Test]
        public void ReturnCorrectValue_WhenCanHandlerReturnsFalse_AndNextCommandResolutionHandlerIsNull()
        {
            // Arrange
            var commandResolutionHandler = new OverriddenCanHandleCommandMethodMockCommandResolutionHandler();

            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            commandResolutionHandler.CanHandleCommandMethodReturnValue = false;

            var expectedResult = combatStatisticsContainer.Object;

            // Act
            var actualResult = commandResolutionHandler.ResolveCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            Assert.That(actualResult, Is.SameAs(expectedResult));
        }
    }
}
