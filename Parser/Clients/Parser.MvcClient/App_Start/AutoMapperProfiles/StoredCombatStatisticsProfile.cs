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
            this.CreateMap<StoredCombatStatisticsProjection, StoredCombatStatistics>();
            this.CreateMap<StoredCombatStatistics, StoredCombatStatisticsProjection>();
            this.CreateMap<FinalizedCombatStatistics, StoredCombatStatisticsProjection>();
        }
    }
}