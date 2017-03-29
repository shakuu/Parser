using AutoMapper;

using Parser.Common.Logging.Models;
using Parser.Data.ViewModels.Administration;

namespace Parser.MvcClient.App_Start.AutoMapperProfiles
{
    public class LogEntryProfile : Profile
    {
        public LogEntryProfile()
        {
            this.CreateMap<LogEntry, LogEntryViewModel>();
        }
    }
}