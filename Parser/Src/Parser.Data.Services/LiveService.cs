using Bytes2you.Validation;

using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Factories;
using Parser.Data.ViewModels.Live;
using Parser.LogFile.Parser.Contracts;

namespace Parser.Data.Services
{
    public class LiveService : ILiveService
    {
        private readonly ILogFileParserEngineManager logFileParserEngineManager;
        private readonly ILiveStatisticsViewModelFactory liveStatisticsViewModelFactory;

        public LiveService(ILogFileParserEngineManager logFileParserEngineManager, ILiveStatisticsViewModelFactory liveStatisticsViewModelFactory)
        {
            Guard.WhenArgument(logFileParserEngineManager, nameof(ILogFileParserEngine)).IsNull().Throw();
            Guard.WhenArgument(liveStatisticsViewModelFactory, nameof(ILiveStatisticsViewModelFactory)).IsNull().Throw();

            this.logFileParserEngineManager = logFileParserEngineManager;
            this.liveStatisticsViewModelFactory = liveStatisticsViewModelFactory;
        }

        public LiveStatisticsViewModel GetLiveStatisticsViewModel(string username)
        {
            var logFileParserEngine = this.logFileParserEngineManager.FindLogFileParserEngineByUsername(username);

            var liveCombatStatistics = logFileParserEngine?.GetLiveCombatStatistics();
            if (liveCombatStatistics == null)
            {
                return null;
            }

            var liveStatisticsViewModel = this.liveStatisticsViewModelFactory.CreateLiveStatisticsViewModel();
            liveStatisticsViewModel.CharacterName = liveCombatStatistics.CharacterName;
            liveStatisticsViewModel.CombatDuration = liveCombatStatistics.CombatDuration;
            liveStatisticsViewModel.DamageDonePerSecond = liveCombatStatistics.DamageDonePerSecond;
            liveStatisticsViewModel.HealingDonePerSecond = liveCombatStatistics.HealingDonePerSecond;

            return liveStatisticsViewModel;
        }
    }
}
