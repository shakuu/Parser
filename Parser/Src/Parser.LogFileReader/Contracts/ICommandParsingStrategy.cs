namespace Parser.LogFileReader.Contracts
{
    public interface ICommandParsingStrategy
    {
        ICommand ParseCommand(string input);
    }
}
