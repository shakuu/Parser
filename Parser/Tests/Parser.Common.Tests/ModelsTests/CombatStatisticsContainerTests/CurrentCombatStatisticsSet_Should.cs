using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.Common.Tests.Mocks;

namespace Parser.Common.Tests.ModelsTests.CombatStatisticsContainerTests
{
    [TestFixture]
    public class CurrentCombatStatisticsSet_Should
    {
        [Test]
        public void NotInvokeCurrentCombatStatisticsChangedMethod_WhenPreviousValueWasNull()
        {
            // Arrange
            var currentCombatStatisticsChangedEventHandlerProvider = new Mock<ICurrentCombatStatisticsChangedEventHandlerProvider>();
            var currentCombatStatisticsChangedEventArgsFactory = new Mock<ICurrentCombatStatisticsChangedEventArgsFactory>();

            var combatStatisticsContainer = new MockCombatStatisticsContainer(currentCombatStatisticsChangedEventHandlerProvider.Object, currentCombatStatisticsChangedEventArgsFactory.Object);
            combatStatisticsContainer.MockingCurrentCombatStatistics = null;

            var currentCombatStatistics = new Mock<ICombatStatistics>();

            // Act
            combatStatisticsContainer.CurrentCombatStatistics = currentCombatStatistics.Object;

            // Assert
            Assert.That(combatStatisticsContainer.CurrentCombatStatisticsChangedMethodIsCalled, Is.False);
        }

        [Test]
        public void InvokeCurrentCombatStatisticsChangedMethod_WhenPreviousValueWasNotNull()
        {
            // Arrange
            var currentCombatStatisticsChangedEventHandlerProvider = new Mock<ICurrentCombatStatisticsChangedEventHandlerProvider>();
            var currentCombatStatisticsChangedEventArgsFactory = new Mock<ICurrentCombatStatisticsChangedEventArgsFactory>();

            var combatStatisticsContainer = new MockCombatStatisticsContainer(currentCombatStatisticsChangedEventHandlerProvider.Object, currentCombatStatisticsChangedEventArgsFactory.Object);

            var currentCombatStatistics = new Mock<ICombatStatistics>();

            combatStatisticsContainer.MockingCurrentCombatStatistics = currentCombatStatistics.Object;

            // Act
            combatStatisticsContainer.CurrentCombatStatistics = currentCombatStatistics.Object;

            // Assert
            Assert.That(combatStatisticsContainer.CurrentCombatStatisticsChangedMethodIsCalled, Is.True);
        }

        [Test]
        public void InvokeCurrentCombatStatisticsChangedMethodWithCorrectParameter_WhenPreviousValueWasNotNull()
        {
            // Arrange
            var currentCombatStatisticsChangedEventHandlerProvider = new Mock<ICurrentCombatStatisticsChangedEventHandlerProvider>();
            var currentCombatStatisticsChangedEventArgsFactory = new Mock<ICurrentCombatStatisticsChangedEventArgsFactory>();

            var combatStatisticsContainer = new MockCombatStatisticsContainer(currentCombatStatisticsChangedEventHandlerProvider.Object, currentCombatStatisticsChangedEventArgsFactory.Object);

            var currentCombatStatistics = new Mock<ICombatStatistics>();

            combatStatisticsContainer.MockingCurrentCombatStatistics = currentCombatStatistics.Object;

            var updatedCurrentCombatStatistics = new Mock<ICombatStatistics>();

            // Act
            combatStatisticsContainer.CurrentCombatStatistics = updatedCurrentCombatStatistics.Object;

            // Assert
            Assert.That(combatStatisticsContainer.CurrentCombatStatisticsChangedMethodICombatStatisticsParameter, Is.SameAs(currentCombatStatistics.Object));
        }
    }
}
