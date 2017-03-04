using System;

using Parser.Common.Contracts;

namespace Parser.LogFileReader.Contracts
{
    public interface ICommandUtilizationStrategy : IDisposable
    {
        void UtilizeCommand(ICommand command);
    }
}
