using System;

using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ILogFileParserEngine
    {
        event EventHandler OnExitCombat;

        void EnqueueCommand(ICommand command);

        ICombatStatisticsContainer GetComabtStatistics();
    }
}
