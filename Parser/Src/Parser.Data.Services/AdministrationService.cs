using System;
using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.Logging;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels.Administration;

namespace Parser.Data.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly ILogReportingDataProvider logReportingDataProvider;
        private readonly IObjectMapperProvider objectMapperProvider;

        public AdministrationService(ILogReportingDataProvider logReportingDataProvider, IObjectMapperProvider objectMapperProvider)
        {
            Guard.WhenArgument(logReportingDataProvider, nameof(ILogReportingDataProvider)).IsNull().Throw();
            Guard.WhenArgument(objectMapperProvider, nameof(IObjectMapperProvider)).IsNull().Throw();

            this.logReportingDataProvider = logReportingDataProvider;
            this.objectMapperProvider = objectMapperProvider;
        }

        public IEnumerable<LogEntryViewModel> GetErrorsForPeriod(int periodInHours)
        {
            var logEntries = this.logReportingDataProvider.GetErrorsForPeriod(periodInHours);

            var viewModels = this.objectMapperProvider.Map<IEnumerable<LogEntryViewModel>>(logEntries);

            return viewModels;
        }
    }
}
