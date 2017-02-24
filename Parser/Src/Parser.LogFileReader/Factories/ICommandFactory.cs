using Parser.LogFileReader.Contracts;

namespace Parser.LogFileReader.Factories
{
    public interface ICommandFactory
    {
        ICommand CreateCommand();
    }
}
