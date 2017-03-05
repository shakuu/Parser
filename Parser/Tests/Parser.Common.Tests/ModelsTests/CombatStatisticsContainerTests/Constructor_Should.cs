using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.Factories;
using Parser.Common.Models;

namespace Parser.Common.Tests.ModelsTests.CombatStatisticsContainerTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectICombatStatisticsContainerInstance_WhenParametersAreValid()
        {
            // Arrange
            var currentCombatStatisticsChangedEventHandlerProvider = new Mock<ICurrentCombatStatisticsChangedEventHandlerProvider>();
            var currentCombatStatisticsChangedEventArgsFactory = new Mock<ICurrentCombatStatisticsChangedEventArgsFactory>();

            // Act
            var actualInstance = new CombatStatisticsContainer(currentCombatStatisticsChangedEventHandlerProvider.Object, currentCombatStatisticsChangedEventArgsFactory.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<ICombatStatisticsContainer>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenICurrentCombatStatisticsChangedEventHandlerProviderParameterIsNull()
        {
            // Arrange
            ICurrentCombatStatisticsChangedEventHandlerProvider currentCombatStatisticsChangedEventHandlerProvider = null;
            var currentCombatStatisticsChangedEventArgsFactory = new Mock<ICurrentCombatStatisticsChangedEventArgsFactory>();

            // Act & Assert
            Assert.That(
                () => new CombatStatisticsContainer(currentCombatStatisticsChangedEventHandlerProvider, currentCombatStatisticsChangedEventArgsFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICurrentCombatStatisticsChangedEventHandlerProvider)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenICurrentCombatStatisticsChangedEventArgsFactoryParameterIsNull()
        {
            // Arrange
            var currentCombatStatisticsChangedEventHandlerProvider = new Mock<ICurrentCombatStatisticsChangedEventHandlerProvider>();
            ICurrentCombatStatisticsChangedEventArgsFactory currentCombatStatisticsChangedEventArgsFactory = null;

            // Act & Assert
            Assert.That(
                () => new CombatStatisticsContainer(currentCombatStatisticsChangedEventHandlerProvider.Object, currentCombatStatisticsChangedEventArgsFactory),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ICurrentCombatStatisticsChangedEventArgsFactory)));
        }
    }
}
