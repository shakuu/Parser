using Parser.Common.Contracts;
using Parser.LogFile.Parser.EventsArgs;

namespace Parser.LogFile.Parser.Factories
{
    public interface IExitCombatEventArgsFactory
    {
        ExitCombatEventArgs CreateExitCombatEventArgs(ICombatStatistics combatStatistics);
    }
}
