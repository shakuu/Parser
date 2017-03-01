using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ICommandResolutionStrategy
    {
        void ResolveCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer);
    }
}
