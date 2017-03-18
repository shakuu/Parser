using Parser.Common.Contracts;

namespace Parser.LogFileParser.Contracts
{
    public interface ICommandResolutionHandler
    {
        ICombatStatisticsContainer ResolveCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer);
    }
}
