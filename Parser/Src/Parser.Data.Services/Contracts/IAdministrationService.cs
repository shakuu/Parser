using System.Collections.Generic;

using Parser.Data.ViewModels.Administration;

namespace Parser.Data.Services.Contracts
{
    public interface IAdministrationService
    {
        IEnumerable<LogEntryViewModel> GetErrorsForPeriod(int periodInHours);
    }
}
