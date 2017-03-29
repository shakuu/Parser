using System;
using Parser.LogFile.Parser.EventsArgs;

namespace Parser.LogFile.Parser.Contracts
{
    public interface IExitCombatNotification
    {
        event EventHandler<ExitCombatEventArgs> OnExitCombat;
    }
}
