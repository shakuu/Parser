using System;

namespace Parser.LogFile.Reader.Contracts
{
    public interface IForceCommandUtilizationStrategy : ICommandUtilizationStrategy, IDisposable
    {
        void ForceUtilizeCommand();
    }
}
