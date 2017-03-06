using AutoMapper;

using Parser.Data.Models;
using Parser.Data.Projections;

namespace Parser.MvcClient.App_Start.AutoMapperProfiles
{
    public class ParserUserProfile : Profile
    {
        public ParserUserProfile()
        {
            this.CreateMap<ParserUser, ParserUserProjection>();
            this.CreateMap<ParserUserProjection, ParserUser>();
        }
    }
}