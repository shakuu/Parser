using Parser.Common.Contracts;

namespace Parser.LogFile.Parser.Contracts
{
    public interface ICommandResolutionHandler
    {
        ICombatStatisticsContainer ResolveCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer);
    }
}
