using System;

using Bytes2you.Validation;

using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Live;
using Parser.LogFile.Parser.Contracts;

namespace Parser.Data.Services
{
    public class LiveService : ILiveService
    {
        private readonly ILogFileParserEngineManager logFileParserEngineManager;

        public LiveService(ILogFileParserEngineManager logFileParserEngineManager)
        {
            Guard.WhenArgument(logFileParserEngineManager, nameof(ILogFileParserEngine)).IsNull().Throw();

            this.logFileParserEngineManager = logFileParserEngineManager;
        }

        public LiveStatisticsViewModel GetLiveStatisticsViewModel(string username)
        {
            throw new NotImplementedException();
        }
    }
}
