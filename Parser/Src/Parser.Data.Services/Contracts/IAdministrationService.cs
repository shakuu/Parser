using System.Collections.Generic;

using Parser.Common.Logging;
using Parser.Data.ViewModels.Administration;

namespace Parser.Data.Services.Contracts
{
    public interface IAdministrationService
    {
        IEnumerable<LogEntryViewModel> GetLogEntriesForPeriod(int periodInHours, MessageType messageType);
    }
}
