using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.Models;
using Parser.Data.ViewModels;

namespace Parser.Data.DataProviders
{
    public class StoredCombatStatisticsDataProvider : IStoredCombatStatisticsDataProvider
    {
        private readonly IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository;
        private readonly IObjectMapperProvider objectMapperProvider;

        public StoredCombatStatisticsDataProvider(IEntityFrameworkRepository<StoredCombatStatistics> storedCombatStatisticsEntityFrameworkRepository, IObjectMapperProvider objectMapperProvider)
        {
            Guard.WhenArgument(storedCombatStatisticsEntityFrameworkRepository, nameof(IEntityFrameworkRepository<StoredCombatStatistics>)).IsNull().Throw();
            Guard.WhenArgument(objectMapperProvider, nameof(IObjectMapperProvider)).IsNull().Throw();

            this.storedCombatStatisticsEntityFrameworkRepository = storedCombatStatisticsEntityFrameworkRepository;
            this.objectMapperProvider = objectMapperProvider;
        }

        public StoredCombatStatisticsViewModel CreateStoredCombatStatistics(StoredCombatStatisticsViewModel model)
        {
            Guard.WhenArgument(model, nameof(StoredCombatStatisticsViewModel)).IsNull().Throw();

            var storedCombatStatistics = this.objectMapperProvider.Map<StoredCombatStatistics>(model);

            var dbStoredCombatStatistics = this.storedCombatStatisticsEntityFrameworkRepository.Create(storedCombatStatistics);

            var viewModel = this.objectMapperProvider.Map<StoredCombatStatisticsViewModel>(dbStoredCombatStatistics);

            return viewModel;
        }
    }
}
