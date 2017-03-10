using Parser.Data.ViewModels;

namespace Parser.Data.Contracts
{
    public interface IParserUserDataProvider
    {
        ParserUserViewModel CreateParserUser(ParserUserViewModel model);
    }
}
