using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using Parser.Data.Contracts;
using Parser.Data.Models;
using Parser.Data.ViewModels.Leaderboard;

namespace Parser.Data.DataProviders
{
    public class OutputPerSecondViewModelDataProvider : Contracts.OutputPerSecondViewModelDataProvider
    {
        private const int DefaultPageSize = 5;
        private const int DefaultPageNumber = 1;

        private readonly IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository;

        public OutputPerSecondViewModelDataProvider(IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository)
        {
            Guard.WhenArgument(storedCombatStatisticsEntityFrameworkRepository, nameof(storedCombatStatisticsEntityFrameworkRepository)).IsNull().Throw();

            this.storedCombatStatisticsEntityFrameworkRepository = storedCombatStatisticsEntityFrameworkRepository;
        }

        public IList<OutputPerSecondViewModel> GetTopDamageOnPage(int pageNumber, int pageSize)
        {
            pageNumber = this.ValidatePageNumber(pageNumber);
            pageSize = this.ValidatePageSize(pageSize);

            return this.GetDamageOutputPerSecondViewModels(pageNumber, pageSize);
        }

        public IList<OutputPerSecondViewModel> GetTopHealingOnPage(int pageNumber, int pageSize)
        {
            pageNumber = this.ValidatePageNumber(pageNumber);
            pageSize = this.ValidatePageSize(pageSize);

            return this.GetHealingOutputPerSecondViewModels(pageNumber, pageSize);
        }

        private int ValidatePageNumber(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                pageNumber = OutputPerSecondViewModelDataProvider.DefaultPageNumber;
            }

            return pageNumber;
        }

        private int ValidatePageSize(int pageSize)
        {
            if (pageSize <= 0)
            {
                pageSize = OutputPerSecondViewModelDataProvider.DefaultPageSize;
            }

            return pageSize;
        }

        private IList<OutputPerSecondViewModel> GetDamageOutputPerSecondViewModels(int pageNumber, int pageSize)
        {
            return this.storedCombatStatisticsEntityFrameworkRepository.Entities
                .OrderByDescending(e => e.DamageDonePerSecond)
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .Select(e => new OutputPerSecondViewModel() { Id = e.Id, CharacterName = e.CharacterName, OutputPerSecond = e.DamageDonePerSecond })
                .ToList();
        }

        private IList<OutputPerSecondViewModel> GetHealingOutputPerSecondViewModels(int pageNumber, int pageSize)
        {
            return this.storedCombatStatisticsEntityFrameworkRepository.Entities
                .OrderByDescending(e => e.HealingDonePerSecond)
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .Select(e => new OutputPerSecondViewModel() { Id = e.Id, CharacterName = e.CharacterName, OutputPerSecond = e.HealingDonePerSecond })
                .ToList();
        }
    }
}
