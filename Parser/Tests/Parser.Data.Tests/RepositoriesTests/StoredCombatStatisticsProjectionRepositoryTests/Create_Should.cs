using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.Models;
using Parser.Data.ViewModels;
using Parser.Data.Repositories;

namespace Parser.Data.Tests.RepositoriesTests.StoredCombatStatisticsProjectionRepositoryTests
{
    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenStoredCombatStatisticsProjectionParameterIsNull()
        {
            // Arrange
            var storedCombatStatisticsRepository = new Mock<IStoredCombatStatisticsRepository>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var storedCombatStatisticsProjectionRepository = new StoredCombatStatisticsProjectionRepository(storedCombatStatisticsRepository.Object, objectMapperProvider.Object);

            StoredCombatStatisticsViewModel storedCombatStatisticsProjection = null;

            // Act & Assert
            Assert.That(
                () => storedCombatStatisticsProjectionRepository.Create(storedCombatStatisticsProjection),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(StoredCombatStatisticsViewModel)));
        }

        [Test]
        public void InvokeIObjectMapperProvider_MapMethodOnceWithCorrectParameters()
        {
            // Arrange
            var storedCombatStatisticsRepository = new Mock<IStoredCombatStatisticsRepository>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var storedCombatStatisticsProjectionRepository = new StoredCombatStatisticsProjectionRepository(storedCombatStatisticsRepository.Object, objectMapperProvider.Object);

            var storedCombatStatisticsProjection = new StoredCombatStatisticsViewModel();

            // Act 
            storedCombatStatisticsProjectionRepository.Create(storedCombatStatisticsProjection);

            // Assert
            objectMapperProvider.Verify(p => p.Map<StoredCombatStatistics>(storedCombatStatisticsProjection), Times.Once);
        }

        [Test]
        public void InvokeIStoredCombatStatisticsRepository_CreateMethodOnceWithCorrectParameter()
        {
            // Arrange
            var storedCombatStatisticsRepository = new Mock<IStoredCombatStatisticsRepository>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var storedCombatStatisticsProjectionRepository = new StoredCombatStatisticsProjectionRepository(storedCombatStatisticsRepository.Object, objectMapperProvider.Object);

            var storedCombatStatisticsProjection = new StoredCombatStatisticsViewModel();

            var storedCombatStatistics = new StoredCombatStatistics();
            objectMapperProvider.Setup(p => p.Map<StoredCombatStatistics>(It.IsAny<StoredCombatStatisticsViewModel>())).Returns(storedCombatStatistics);

            // Act 
            storedCombatStatisticsProjectionRepository.Create(storedCombatStatisticsProjection);

            // Assert
            storedCombatStatisticsRepository.Verify(r => r.Create(storedCombatStatistics), Times.Once);
        }

        [Test]
        public void ReturnsCorrectStoredCombatStatisticsProjectionInstance()
        {
            // Arrange
            var storedCombatStatisticsRepository = new Mock<IStoredCombatStatisticsRepository>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var storedCombatStatisticsProjectionRepository = new StoredCombatStatisticsProjectionRepository(storedCombatStatisticsRepository.Object, objectMapperProvider.Object);

            var storedCombatStatisticsProjection = new StoredCombatStatisticsViewModel();

            // Act 
            var actualReturnedStoredCombatStatisticsProjectionInstance = storedCombatStatisticsProjectionRepository.Create(storedCombatStatisticsProjection);

            // Assert
            Assert.That(actualReturnedStoredCombatStatisticsProjectionInstance, Is.SameAs(storedCombatStatisticsProjection));
        }
    }
}
