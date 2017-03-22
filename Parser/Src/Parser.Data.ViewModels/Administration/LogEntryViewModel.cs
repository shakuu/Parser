using System;

namespace Parser.Data.ViewModels.Administration
{
    public class LogEntryViewModel
    {
        string Message { get; set; }

        DateTime Timestamp { get; set; }

        string Action { get; set; }

        string Controller { get; set; }
    }
}
