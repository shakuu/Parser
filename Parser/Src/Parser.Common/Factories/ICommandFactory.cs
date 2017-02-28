using Parser.Common.Contracts;

namespace Parser.Common.Factories
{
    public interface ICommandFactory
    {
        ICommand CreateCommand();
    }
}
