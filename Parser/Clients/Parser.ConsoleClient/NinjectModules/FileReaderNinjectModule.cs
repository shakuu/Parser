﻿using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;

using Parser.ConsoleClient.FileReaderImplementations;

using Parser.FileReader.Contracts;

namespace Parser.ConsoleClient.NinjectModules
{
    internal class FileReaderNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindFactoriesByConvention);
            this.Bind(this.BindAllClassesByConvention);

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
                .FromAssembliesMatching("*.FileReader.*")
                .SelectAllInterfaces()
                .EndingWith("Factory")
                .BindToFactory()
                .Configure(f => f.InSingletonScope());
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.FileReader.*")
                .SelectAllClasses()
                .BindDefaultInterface();
        }
    }
}
