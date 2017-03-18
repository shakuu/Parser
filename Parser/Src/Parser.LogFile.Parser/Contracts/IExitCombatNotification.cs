using System;

using Parser.LogFileParser.EventsArgs;

namespace Parser.LogFileParser.Contracts
{
    public interface IExitCombatNotification
    {
        event EventHandler<ExitCombatEventArgs> OnExitCombat;
    }
}
