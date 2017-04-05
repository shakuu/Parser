using System;

using Moq;
using NUnit.Framework;

using Parser.Data.Contracts;
using Parser.Data.DataProviders;
using Parser.Data.Models;

namespace Parser.Data.Tests.DataProvidersTests.OutputPerSecondViewModelDataProviderTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateIOutputPerSecondViewModelDataProviderInstance_WhenParametersAreValid()
        {
            // Arrange
            var storedCombatStatisticsEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<StoredCombatStatistics>>();

            // Act
            var actualInstance = new OutputPerSecondViewModelDataProvider(storedCombatStatisticsEntityFrameworkRepository.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<IOutputPerSecondViewModelDataProvider>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIEntityFrameworkRepositoryParameterIsNull()
        {
            // Arrange
            IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository = null;

            // Act & Assert
            Assert.That(
                () => new OutputPerSecondViewModelDataProvider(storedCombatStatisticsEntityFrameworkRepository),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(storedCombatStatisticsEntityFrameworkRepository)));
        }
    }
}
