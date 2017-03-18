using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.LogFile.Parser.Tests.Mocks;

namespace Parser.LogFile.Parser.Tests.CommandResolutionHandlersTests.DamageCommandResolutionHandlerTests
{
    [TestFixture]
    public class HandleCommand_Should
    {
        [Test]
        public void IncrementICombatStatisticsContainerParameter_CurrentCombatStatisticsDamageDonePropertyWithCorrectAmount()
        {
            // Arrange
            var damageCommandResolutionHandler = new MockDamageCommandResolutionHandler();

            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            var matchingEventName = "Damage";
            var expectedIncrementAmount = 42;

            command.SetupGet(c => c.EventName).Returns(matchingEventName);
            command.SetupGet(c => c.EffectEffectiveAmount).Returns(expectedIncrementAmount);

            var currentCombatStatistics = new Mock<ICombatStatistics>();
            combatStatisticsContainer.SetupGet(c => c.CurrentCombatStatistics).Returns(currentCombatStatistics.Object);

            // Act
            damageCommandResolutionHandler.HandleCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            currentCombatStatistics.VerifySet(s => s.DamageDone += expectedIncrementAmount, Times.Once);
        }

        [Test]
        public void ReturnTheSameICombatStatisticsContainerInstance()
        {
            // Arrange
            var damageCommandResolutionHandler = new MockDamageCommandResolutionHandler();

            var command = new Mock<ICommand>();
            var combatStatisticsContainer = new Mock<ICombatStatisticsContainer>();

            var currentCombatStatistics = new Mock<ICombatStatistics>();
            combatStatisticsContainer.SetupGet(c => c.CurrentCombatStatistics).Returns(currentCombatStatistics.Object);

            var expectedReturnedICombatStatisticsContainerInstance = combatStatisticsContainer.Object;

            // Act
            var actualReturnedICombatStatisticsContainerInstance = damageCommandResolutionHandler.HandleCommand(command.Object, combatStatisticsContainer.Object);

            // Assert
            Assert.That(actualReturnedICombatStatisticsContainerInstance, Is.SameAs(expectedReturnedICombatStatisticsContainerInstance));
        }
    }
}
