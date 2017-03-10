using AutoMapper;

using Parser.Data.Models;
using Parser.Data.ViewModels;

namespace Parser.MvcClient.App_Start.AutoMapperProfiles
{
    public class ParserUserProfile : Profile
    {
        public ParserUserProfile()
        {
            this.CreateMap<ParserUser, ParserUserViewModel>();
            this.CreateMap<ParserUserViewModel, ParserUser>();

            this.CreateMap<ParserUser, RegisterParserUserViewModel>();
            this.CreateMap<RegisterParserUserViewModel, ParserUser>();
        }
    }
}