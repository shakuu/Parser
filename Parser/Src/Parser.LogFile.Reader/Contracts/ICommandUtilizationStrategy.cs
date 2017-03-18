using System;

using Parser.Common.Contracts;

namespace Parser.LogFile.Reader.Contracts
{
    public interface ICommandUtilizationStrategy : IDisposable
    {
        void UtilizeCommand(ICommand command);
    }
}
