using System.Collections.Generic;
using System.Linq;

using Moq;
using NUnit.Framework;

using Parser.Data.Contracts;
using Parser.Data.DataProviders;
using Parser.Data.Models;

namespace Parser.Data.Tests.DataProvidersTests.OutputPerSecondViewModelDataProviderTests
{
    [TestFixture]
    public class GetTopHealingOnPageInDescendingOrder_Should
    {
        [Test]
        public void ReturnCorrectOutputPersecondValues()
        {
            // Arrange
            var storedCombatStatisticsEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<StoredCombatStatistics>>();

            var outputPerSecondViewModelDataProvider = new OutputPerSecondViewModelDataProvider(storedCombatStatisticsEntityFrameworkRepository.Object);

            var pageNumber = 0;
            var pageSize = 5;

            var fakeData = new List<StoredCombatStatistics>()
            {
                new StoredCombatStatistics() { HealingDonePerSecond = 5 },
                new StoredCombatStatistics() { HealingDonePerSecond = 4 },
                new StoredCombatStatistics() { HealingDonePerSecond = 3 },
                new StoredCombatStatistics() { HealingDonePerSecond = 2 },
                new StoredCombatStatistics() { HealingDonePerSecond = 1 },
            };

            var fakeDataQueryable = fakeData.AsQueryable();
            storedCombatStatisticsEntityFrameworkRepository.SetupGet(r => r.Entities).Returns(fakeDataQueryable);

            // Act
            var actualResult = outputPerSecondViewModelDataProvider.GetTopHealingOnPageInDescendingOrder(pageNumber, pageSize);

            // Assert
            for (int index = 0, expectedValue = 5; index < fakeData.Count; index++, expectedValue--)
            {
                Assert.That(actualResult[index].OutputPerSecond, Is.EqualTo(fakeData[index].HealingDonePerSecond));
            }
        }
    }
}
