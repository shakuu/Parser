using Parser.FileReader.Contracts;

namespace Parser.FileReader.Factories
{
    public interface ICommandFactory
    {
        ICommand CreateCommand();
    }
}
