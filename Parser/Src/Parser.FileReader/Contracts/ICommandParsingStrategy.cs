namespace Parser.FileReader.Contracts
{
    public interface ICommandParsingStrategy
    {
        ICommand ParseCommand(string input);
    }
}
