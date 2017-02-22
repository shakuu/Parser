namespace Parser.FileReader.Contracts
{
    public interface ICommandParsingStrategy
    {
        ICommand ParseInputCommand(string input);
    }
}
