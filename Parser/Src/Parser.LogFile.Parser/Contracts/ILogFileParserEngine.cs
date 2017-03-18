using Parser.Common.Contracts;

namespace Parser.LogFile.Parser.Contracts
{
    public interface ILogFileParserEngine
    {
        void EnqueueCommand(ICommand command);

        ICombatStatisticsContainer GetCombatStatistics();
    }
}
