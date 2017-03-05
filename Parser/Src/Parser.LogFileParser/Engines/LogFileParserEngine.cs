using System;

using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.EventsArgs;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.EventsArgs;
using Parser.LogFileParser.Factories;

namespace Parser.LogFileParser.Engines
{
    public class LogFileParserEngine : ILogFileParserEngine
    {
        public event EventHandler<ExitCombatEventArgs> OnExitCombat;

        private readonly ICommandResolutionHandler commandResolutionHandler;
        private readonly IExitCombatEventArgsFactory exitCombatEventArgsFactory;

        private ICombatStatisticsContainer combatStatisticsContainer;

        public LogFileParserEngine(ICommandResolutionHandler commandResolutionHandler, ICombatStatisticsContainer combatStatisticsContainer, IExitCombatEventArgsFactory exitCombatEventArgsFactory)
        {
            Guard.WhenArgument(commandResolutionHandler, nameof(ICommandResolutionHandler)).IsNull().Throw();
            Guard.WhenArgument(combatStatisticsContainer, nameof(ICombatStatisticsContainer)).IsNull().Throw();
            Guard.WhenArgument(exitCombatEventArgsFactory, nameof(IExitCombatEventArgsFactory)).IsNull().Throw();

            this.commandResolutionHandler = commandResolutionHandler;
            this.combatStatisticsContainer = combatStatisticsContainer;
            this.exitCombatEventArgsFactory = exitCombatEventArgsFactory;

            this.combatStatisticsContainer.OnCurrentCombatStatisticsChanged += this.OnCurrentCombatStatisticsChanged;
        }

        protected ICombatStatisticsContainer CombatStatisticsContainer { get { return this.combatStatisticsContainer; } set { this.combatStatisticsContainer = value; } }

        public void EnqueueCommand(ICommand command)
        {
            Guard.WhenArgument(command, nameof(ICommand)).IsNull().Throw();

            this.combatStatisticsContainer = this.commandResolutionHandler.ResolveCommand(command, this.combatStatisticsContainer);
        }

        public ICombatStatisticsContainer GetComabtStatistics()
        {
            return this.combatStatisticsContainer;
        }

        private void OnCurrentCombatStatisticsChanged(object sender, CurrentCombatStatisticsChangedEventArgs args)
        {
            Guard.WhenArgument(args, nameof(CurrentCombatStatisticsChangedEventArgs)).IsNull().Throw();
            Guard.WhenArgument(args.CombatStatistics, nameof(ICombatStatistics)).IsNull().Throw();

            var exitCombatEventArgs = this.exitCombatEventArgsFactory.CreateExitCombatEventArgs(args.CombatStatistics);

            this.OnExitCombat?.Invoke(this, exitCombatEventArgs);
        }
    }
}
