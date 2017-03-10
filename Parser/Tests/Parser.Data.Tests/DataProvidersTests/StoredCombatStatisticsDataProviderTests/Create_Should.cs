using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.DataProviders;
using Parser.Data.Models;
using Parser.Data.ViewModels;

namespace Parser.Data.Tests.DataProvidersTests.StoredCombatStatisticsDataProviderTests
{
    [TestFixture]
    public class Create_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenStoredCombatStatisticsViewModelParameterIsNull()
        {
            // Arrange
            var storedCombatStatisticsEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<StoredCombatStatistics>>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var storedCombatStatisticsDataProvider = new StoredCombatStatisticsDataProvider(storedCombatStatisticsEntityFrameworkRepository.Object, objectMapperProvider.Object);

            StoredCombatStatisticsViewModel model = null;

            // Act & Assert
            Assert.That(
                () => storedCombatStatisticsDataProvider.Create(model),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(StoredCombatStatisticsViewModel)));
        }

        [Test]
        public void InvokeIObjectMapperProvider_MapStoredCombatStatisticsOnceWithCorrectParameter()
        {
            // Arrange
            var storedCombatStatisticsEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<StoredCombatStatistics>>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var storedCombatStatisticsDataProvider = new StoredCombatStatisticsDataProvider(storedCombatStatisticsEntityFrameworkRepository.Object, objectMapperProvider.Object);

            var model = new StoredCombatStatisticsViewModel();

            // Act
            storedCombatStatisticsDataProvider.Create(model);

            // Assert
            objectMapperProvider.Verify(p => p.Map<StoredCombatStatistics>(model), Times.Once);
        }

        [Test]
        public void InvokeIEntityFrameworkRepository_CreateMethodOnceWithCorrectParameter()
        {
            // Arrange
            var storedCombatStatisticsEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<StoredCombatStatistics>>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var storedCombatStatisticsDataProvider = new StoredCombatStatisticsDataProvider(storedCombatStatisticsEntityFrameworkRepository.Object, objectMapperProvider.Object);

            var model = new StoredCombatStatisticsViewModel();

            var storedCombatStatistics = new StoredCombatStatistics();
            objectMapperProvider.Setup(p => p.Map<StoredCombatStatistics>(It.IsAny<StoredCombatStatisticsViewModel>())).Returns(storedCombatStatistics);

            // Act
            storedCombatStatisticsDataProvider.Create(model);

            // Assert
            storedCombatStatisticsEntityFrameworkRepository.Verify(r => r.Create(storedCombatStatistics), Times.Once);
        }

        [Test]
        public void InvokeIObjectMapperProvider_MapStoredCombatStatisticsViewModelOnceWithCorrectParameter()
        {
            // Arrange
            var storedCombatStatisticsEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<StoredCombatStatistics>>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var storedCombatStatisticsDataProvider = new StoredCombatStatisticsDataProvider(storedCombatStatisticsEntityFrameworkRepository.Object, objectMapperProvider.Object);

            var model = new StoredCombatStatisticsViewModel();

            var storedCombatStatistics = new StoredCombatStatistics();
            storedCombatStatisticsEntityFrameworkRepository.Setup(r => r.Create(It.IsAny<StoredCombatStatistics>())).Returns(storedCombatStatistics);

            // Act
            storedCombatStatisticsDataProvider.Create(model);

            // Assert
            objectMapperProvider.Verify(p => p.Map<StoredCombatStatisticsViewModel>(storedCombatStatistics), Times.Once);
        }

        [Test]
        public void ReturnCorrectStoredCombatStatisticsViewModelInstance()
        {
            // Arrange
            var storedCombatStatisticsEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<StoredCombatStatistics>>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var storedCombatStatisticsDataProvider = new StoredCombatStatisticsDataProvider(storedCombatStatisticsEntityFrameworkRepository.Object, objectMapperProvider.Object);

            var model = new StoredCombatStatisticsViewModel();

            var expectedReturnedStoredCombatStatisticsViewModelInstance = new StoredCombatStatisticsViewModel();
            objectMapperProvider.Setup(p => p.Map<StoredCombatStatisticsViewModel>(It.IsAny<StoredCombatStatistics>())).Returns(expectedReturnedStoredCombatStatisticsViewModelInstance);

            // Act
            var actualReturnedStoredCombatStatisticsViewModelInstance = storedCombatStatisticsDataProvider.Create(model);

            // Assert
            Assert.That(actualReturnedStoredCombatStatisticsViewModelInstance, Is.SameAs(expectedReturnedStoredCombatStatisticsViewModelInstance));
        }
    }
}
