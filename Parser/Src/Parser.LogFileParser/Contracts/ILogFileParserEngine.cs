using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ILogFileParserEngine : IExitCombatNotification
    {
        void EnqueueCommand(ICommand command);

        ICombatStatisticsContainer GetCombatStatistics();
    }
}
