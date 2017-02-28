using Parser.Common.Contracts;

namespace Parser.LogFileReader.Contracts
{
    public interface ICommandUtilizationStrategy
    {
        void UtilizeCommand(ICommand command);
    }
}
