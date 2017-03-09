using AutoMapper;

using Parser.Common.Models;
using Parser.Data.Models;
using Parser.Data.ViewModels;

namespace Parser.MvcClient.App_Start.AutoMapperProfiles
{
    public class StoredCombatStatisticsProfile : Profile
    {
        public StoredCombatStatisticsProfile()
        {
            // TODO: 
            this.CreateMap<FinalizedCombatStatistics, StoredCombatStatisticsViewModel>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.IsDeleted, opts => opts.Ignore())
                .ForMember(dest => dest.ParserUserId, opts => opts.Ignore())
                .ForMember(dest => dest.ParserUser, opts => opts.Ignore());

            this.CreateMap<StoredCombatStatisticsViewModel, StoredCombatStatistics>();
            this.CreateMap<StoredCombatStatistics, StoredCombatStatisticsViewModel>();
        }
    }
}