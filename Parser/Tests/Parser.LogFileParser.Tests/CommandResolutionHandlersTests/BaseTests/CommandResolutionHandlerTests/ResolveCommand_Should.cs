using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
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
            var commandResolutionHandler = new MockCommandResolutionHandler();

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
            var commandResolutionHandler = new MockCommandResolutionHandler();

            var command = new Mock<ICommand>();
            ICombatStatisticsContainer combatStatisticsContainer = null;

            // Act & Assert
            Assert.That(
                () => commandResolutionHandler.ResolveCommand(command.Object, combatStatisticsContainer),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICombatStatisticsContainer)));
        }
    }
}
