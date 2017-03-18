using Parser.Common.Contracts;

namespace Parser.LogFile.Reader.Contracts
{
    public interface ICommandParsingStrategy
    {
        ICommand ParseCommand(string input);
    }
}
