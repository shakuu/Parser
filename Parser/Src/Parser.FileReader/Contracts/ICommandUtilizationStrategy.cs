namespace Parser.FileReader.Contracts
{
    public interface ICommandUtilizationStrategy
    {
        void UtilizeCommand(ICommand command);
    }
}
