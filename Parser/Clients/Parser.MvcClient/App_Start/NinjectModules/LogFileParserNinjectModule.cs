using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Modules;

using Parser.LogFileParser.CommandResolutionHandlers;
using Parser.LogFileParser.Contracts;
using Parser.LogFileParser.Managers;

namespace Parser.MvcClient.App_Start.NinjectModules
{
    public class LogFileParserNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);
            this.Bind(this.BindFactoriesByConvention);

            this.Bind<ICommandResolutionHandler>().ToMethod(this.CommandResolutionHandlerChainFactoryMethod);
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.LogFileParser.*")
                .SelectAllClasses()
                .BindDefaultInterface()
                .ConfigureFor<LogFileParserEngineManager>(m => m.InSingletonScope());
        }

        private void BindFactoriesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.LogFileParser.*")
                .SelectAllInterfaces()
                .EndingWith("Factory")
                .BindToFactory()
                .Configure(f => f.InSingletonScope());
        }

        private ICommandResolutionHandler CommandResolutionHandlerChainFactoryMethod(IContext context)
        {
            var enterCombatCommandResolutionHandler = context.Kernel.Get<EnterCombatCommandResolutionHandler>();
            var exitCombatCommandResolutionHandler = context.Kernel.Get<ExitCombatCommandResolutionHandler>();
            var damageCombatCommandResolutionHandler = context.Kernel.Get<DamageCommandResolutionHandler>();
            var healCommandResolutionHandler = context.Kernel.Get<HealCommandResolutionHandler>();

            enterCombatCommandResolutionHandler.NextCommandResolutionHandler = exitCombatCommandResolutionHandler;
            exitCombatCommandResolutionHandler.NextCommandResolutionHandler = damageCombatCommandResolutionHandler;
            damageCombatCommandResolutionHandler.NextCommandResolutionHandler = healCommandResolutionHandler;

            return enterCombatCommandResolutionHandler;
        }
    }
}