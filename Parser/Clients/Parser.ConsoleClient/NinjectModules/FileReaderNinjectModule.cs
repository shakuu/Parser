using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Modules;

namespace Parser.ConsoleClient.NinjectModules
{
    internal class FileReaderNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindFactoriesByConvention);
            this.Bind(this.BindAllClassesByConvention);
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
                .BindDefaultInterface()
                .Configure(c => c.InSingletonScope());
        }
    }
}
