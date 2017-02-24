namespace Parser.LogFileReader.Contracts
{
    public interface ICommandUtilizationStrategy
    {
        void UtilizeCommand(ICommand command);
    }
}
