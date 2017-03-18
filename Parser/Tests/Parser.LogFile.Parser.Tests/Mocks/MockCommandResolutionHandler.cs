using System;

using Parser.Common.Contracts;
using Parser.LogFile.Parser.CommandResolutionHandlers.Base;

namespace Parser.LogFile.Parser.Tests.Mocks
{
    internal class MockCommandResolutionHandler : CommandResolutionHandler
    {
        internal MockCommandResolutionHandler(string matchingEventName)
            : base(matchingEventName)
        {
        }

        internal new bool CanHandleCommand(ICommand command)
        {
            return base.CanHandleCommand(command);
        }

        protected override ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            throw new NotImplementedException();
        }
    }
}
