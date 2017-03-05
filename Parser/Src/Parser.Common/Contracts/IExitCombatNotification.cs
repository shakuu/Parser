using System;

namespace Parser.Common.Contracts
{
    public interface IExitCombatNotification
    {
        event EventHandler OnExitCombat;
    }
}
