using Parser.Common.Contracts;
using Parser.LogFileParser.EventsArgs;

namespace Parser.LogFileParser.Factories
{
    public interface IExitCombatEventArgsFactory
    {
        ExitCombatEventArgs CreateExitCombatEventArgs(ICombatStatistics combatStatistics);
    }
}
