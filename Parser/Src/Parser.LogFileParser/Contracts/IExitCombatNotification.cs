using System;

namespace Parser.LogFileParser.Contracts
{
    public interface IExitCombatNotification
    {
        event EventHandler OnExitCombat;
    }
}
