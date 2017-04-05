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
    public class GetTopDamageOnPageInDescendingOrder_Should
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
                new StoredCombatStatistics() { DamageDonePerSecond = 5 },
                new StoredCombatStatistics() { DamageDonePerSecond = 4 },
                new StoredCombatStatistics() { DamageDonePerSecond = 3 },
                new StoredCombatStatistics() { DamageDonePerSecond = 2 },
                new StoredCombatStatistics() { DamageDonePerSecond = 1 },
            };

            var fakeDataQueryable = fakeData.AsQueryable();
            storedCombatStatisticsEntityFrameworkRepository.SetupGet(r => r.Entities).Returns(fakeDataQueryable);

            // Act
            var actualResult = outputPerSecondViewModelDataProvider.GetTopDamageOnPageInDescendingOrder(pageNumber, pageSize);

            // Assert
            for (int index = 0, expectedValue = 5; index < fakeData.Count; index++, expectedValue--)
            {
                Assert.That(actualResult[index].OutputPerSecond, Is.EqualTo(fakeData[index].DamageDonePerSecond));
            }
        }
    }
}
