using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;

using Parser.Common.Contracts;
using Parser.ConsoleClient.FileReaderImplementations;
using Parser.LogFileReader.Contracts;
using Parser.LogFileReader.Strategies;
using Parser.SignalR.Strategies;

namespace Parser.ConsoleClient.NinjectModules
{
    internal class LogFileReaderNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindFactoriesByConvention);
            this.Bind(this.BindAllClassesByConvention);

            //this.Bind<ICommandUtilizationStrategy>().To<SignalRCommandUtilizationStrategy>().InSingletonScope();

            this.Bind<ICommandUtilizationStrategy>().To<ConsoleClientCommandUtilizationStrategy>().InSingletonScope();
            this.Kernel.InterceptReplace<ConsoleClientCommandUtilizationStrategy>(s => s.UtilizeCommand(null), this.ICommandUtilizationStrategyUtilizeCommandMethod);
        }

        private void ICommandUtilizationStrategyUtilizeCommandMethod(IInvocation invocation)
        {
            System.Console.WriteLine((invocation.Request.Arguments[0] as ICommand)?.AbilityActivatorName);
        }

        private void BindFactoriesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.LogFileReader.*")
                .SelectAllInterfaces()
                .EndingWith("Factory")
                .BindToFactory()
                .Configure(f => f.InSingletonScope());
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.LogFileReader.*")
                .SelectAllClasses()
                .BindDefaultInterface()
                // TODO: Remove: Intercepting with testing input
                .ConfigureFor<LogFilePathDiscoveryStrategy>(s => s.Intercept().With<LogFilePathDiscoveryStrategyTestingInterceptor>());
        }
    }
}
