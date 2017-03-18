using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ILogFileParserEngine
    {
        void EnqueueCommand(ICommand command);

        ICombatStatisticsContainer GetCombatStatistics();
    }
}
