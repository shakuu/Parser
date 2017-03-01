using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.LogFileParser.Contracts;

namespace Parser.LogFileParser.CommandResolutionHandlers.Base
{
    public abstract class CommandResolutionHandler : ICommandResolutionHandler, ICommandResolutionHandlerChain
    {
        public ICommandResolutionHandler NextCommandResolutionHandler { get; set; }

        public ICombatStatisticsContainer ResolveCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer)
        {
            Guard.WhenArgument(command, nameof(ICommand)).IsNull().Throw();
            Guard.WhenArgument(combatStatisticsContainer, nameof(ICombatStatisticsContainer)).IsNull().Throw();

            if (this.CanHandleCommand(command))
            {
                return this.HandleCommand(command, combatStatisticsContainer);
            }
            else if (this.NextCommandResolutionHandler != null)
            {
                return this.NextCommandResolutionHandler.ResolveCommand(command, combatStatisticsContainer);
            }
            else
            {
                // TODO: Throw
                return combatStatisticsContainer;
            }
        }

        protected abstract bool CanHandleCommand(ICommand command);

        protected abstract ICombatStatisticsContainer HandleCommand(ICommand command, ICombatStatisticsContainer combatStatisticsContainer);
    }
}
