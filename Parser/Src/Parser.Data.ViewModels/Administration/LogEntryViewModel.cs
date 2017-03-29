using System;

namespace Parser.Data.ViewModels.Administration
{
    public class LogEntryViewModel
    {
        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }
    }
}
