using Parser.Data.ViewModels;

namespace Parser.Data.Services.Contracts
{
    public interface IParserUserService
    {
        ParserUserViewModel CreateParserUser(ParserUserViewModel model);
    }
}
