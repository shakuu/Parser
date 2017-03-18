using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFile.Parser.Tests.Mocks;

namespace Parser.LogFile.Parser.Tests.CommandResolutionHandlersTests.ExitCombatCommandResolutionHandlerTests
{
    [TestFixture]
    public class HandleCommand_Should
    {
        [Test]
        public void InvokeICommandParameter_TimeStampPropertyGetMethodOnce()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            var currentCombatStatistics = new Mock<ICombatStatistics>();
            combatStatisticsContainer.SetupGet(c => c.CurrentCombatStatistics).Returns(currentCombatStatistics.Object);

            var exitCombatCommandResolutionHandler = new MockExitCombatCommandResolutionHandler();

            // Act
            exitCombatCommandResolutionHandler.HandleCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            command.VerifyGet(c => c.TimeStamp, Times.Once);
        }

        [Test]
        public void SetICombatStatisticsContainerParameter_CurrentCombatStatisticsIsCompletedProperty_ToTrue()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            var currentCombatStatistics = new Mock<ICombatStatistics>();
            combatStatisticsContainer.SetupGet(c => c.CurrentCombatStatistics).Returns(currentCombatStatistics.Object);

            var exitCombatCommandResolutionHandler = new MockExitCombatCommandResolutionHandler();

            // Act
            exitCombatCommandResolutionHandler.HandleCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            currentCombatStatistics.VerifySet(s => s.IsCompleted = true, Times.Once);
        }

        [Test]
        public void SetICombatStatisticsContainer_CurrentCombatStatisticsProperty_ToNull()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            var currentCombatStatistics = new Mock<ICombatStatistics>();
            combatStatisticsContainer.SetupGet(c => c.CurrentCombatStatistics).Returns(currentCombatStatistics.Object);

            var exitCombatCommandResolutionHandler = new MockExitCombatCommandResolutionHandler();

            // Act
            exitCombatCommandResolutionHandler.HandleCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            combatStatisticsContainer.VerifySet(c => c.CurrentCombatStatistics = null, Times.Once);
        }

        [Test]
        public void ReturnTheSameICombatStatisticsContainerInstance()
        {
            // Arrange
            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            var currentCombatStatistics = new Mock<ICombatStatistics>();
            combatStatisticsContainer.SetupGet(c => c.CurrentCombatStatistics).Returns(currentCombatStatistics.Object);

            var exitCombatCommandResolutionHandler = new MockExitCombatCommandResolutionHandler();

            var expectedReturnedICombatStatisticsContainerInstance = combatStatisticsContainer.Object;

            // Act
            var actualReturnedICombatStatisticsContainerInstance = exitCombatCommandResolutionHandler.HandleCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            Assert.That(actualReturnedICombatStatisticsContainerInstance, Is.SameAs(expectedReturnedICombatStatisticsContainerInstance));
        }
    }
}
