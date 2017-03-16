using AutoMapper;

using Parser.Common.Models;
using Parser.Data.Models;
using Parser.Data.ViewModels;
using Parser.Data.ViewModels.Leaderboard;

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

            this.CreateMap<StoredCombatStatistics, DamageDonePerSecondViewModel>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
                .ForMember(destination => destination.CharacterName, options => options.MapFrom(source => source.CharacterName))
                .ForMember(destination => destination.DamageDonePerSecond, options => options.MapFrom(source => source.DamageDonePerSecond))
                .ForMember(destination => destination.SvgString, options => options.Ignore());
        }
    }
}