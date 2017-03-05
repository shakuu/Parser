using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.EventsArgs;
using Parser.Common.Factories;
using Parser.Common.Tests.Mocks;

namespace Parser.Common.Tests.ModelsTests.CombatStatisticsContainerTests
{
    [TestFixture]
    public class CurrentCombatStatisticsChanged_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenICombatStatisticsParameterIsNull()
        {
            // Arrange
            var currentCombatStatisticsChangedEventHandlerProvider = new Mock<ICurrentCombatStatisticsChangedEventHandlerProvider>();
            var currentCombatStatisticsChangedEventArgsFactory = new Mock<ICurrentCombatStatisticsChangedEventArgsFactory>();

            var combatStatisticsContainer = new MockCombatStatisticsContainer(currentCombatStatisticsChangedEventHandlerProvider.Object, currentCombatStatisticsChangedEventArgsFactory.Object);

            ICombatStatistics combatStatistics = null;

            // Act & Assert
            Assert.That(
                () => combatStatisticsContainer.MockedCurrentCombatStatisticsChanged(combatStatistics),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICombatStatistics)));
        }

        [Test]
        public void InvokeICurrentCombatStatisticsChangedEventArgsFactory_CreateCurrentCombatStatisticsChangedEventArgsMethodOnceWithCorrectParameter()
        {
            // Arrange
            var currentCombatStatisticsChangedEventHandlerProvider = new Mock<ICurrentCombatStatisticsChangedEventHandlerProvider>();
            var currentCombatStatisticsChangedEventArgsFactory = new Mock<ICurrentCombatStatisticsChangedEventArgsFactory>();

            var combatStatisticsContainer = new MockCombatStatisticsContainer(currentCombatStatisticsChangedEventHandlerProvider.Object, currentCombatStatisticsChangedEventArgsFactory.Object);

            var combatStatistics = new Mock<ICombatStatistics>();

            // Act
            combatStatisticsContainer.MockedCurrentCombatStatisticsChanged(combatStatistics.Object);

            // Assert
            currentCombatStatisticsChangedEventArgsFactory.Verify(f => f.CreateCurrentCombatStatisticsChangedEventArgs(combatStatistics.Object), Times.Once);
        }

        [Test]
        public void InvokeICurrentCombatStatisticsChangedEventHandlerProvider_RaiseMethodOnceWithCorrectParameters()
        {
            // Arrange
            var currentCombatStatisticsChangedEventHandlerProvider = new Mock<ICurrentCombatStatisticsChangedEventHandlerProvider>();
            var currentCombatStatisticsChangedEventArgsFactory = new Mock<ICurrentCombatStatisticsChangedEventArgsFactory>();

            var combatStatisticsContainer = new MockCombatStatisticsContainer(currentCombatStatisticsChangedEventHandlerProvider.Object, currentCombatStatisticsChangedEventArgsFactory.Object);

            var combatStatistics = new Mock<ICombatStatistics>();

            var currentCombatStatisticsChangedEventArgs = new CurrentCombatStatisticsChangedEventArgs(combatStatistics.Object);
            currentCombatStatisticsChangedEventArgsFactory.Setup(f => f.CreateCurrentCombatStatisticsChangedEventArgs(It.IsAny<ICombatStatistics>())).Returns(currentCombatStatisticsChangedEventArgs);
            // Act
            combatStatisticsContainer.MockedCurrentCombatStatisticsChanged(combatStatistics.Object);

            // Assert
            currentCombatStatisticsChangedEventHandlerProvider.Verify(p => p.Raise(combatStatisticsContainer, currentCombatStatisticsChangedEventArgs), Times.Once);
        }
    }
}
