using Parser.Data.ViewModels;

namespace Parser.Data.Services.Contracts
{
    public interface ICreateParserUserService
    {
        ParserUserViewModel CreateParserUser(ParserUserViewModel model);
    }
}
