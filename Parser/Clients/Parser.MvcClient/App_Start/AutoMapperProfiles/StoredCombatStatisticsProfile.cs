using AutoMapper;

using Parser.Common.Models;
using Parser.Data.Models;
using Parser.Data.Projections;

namespace Parser.MvcClient.App_Start.AutoMapperProfiles
{
    public class StoredCombatStatisticsProfile : Profile
    {
        public StoredCombatStatisticsProfile()
        {
            // TODO: 
            this.CreateMap<FinalizedCombatStatistics, StoredCombatStatisticsProjection>()
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.IsDeleted, opts => opts.Ignore())
                .ForMember(dest => dest.ParserUserId, opts => opts.Ignore())
                .ForMember(dest => dest.ParserUser, opts => opts.Ignore());

            this.CreateMap<StoredCombatStatisticsProjection, StoredCombatStatistics>();
            this.CreateMap<StoredCombatStatistics, StoredCombatStatisticsProjection>();
        }
    }
}