using System.Collections.Generic;

using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ILogFileParserEngine
    {
        void EnqueueCommand(ICommand command);

        IEnumerable<ICombatStatisticsContainer> GetComabtStatistics();
    }
}
