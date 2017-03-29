using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Modules;

namespace Parser.WPFClient.NinjectModules
{
    internal class SignalRNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindFactoriesByConvention);
            this.Bind(this.BindAllClassesByConvention);
        }

        private void BindFactoriesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.SignalR.*")
                .SelectAllInterfaces()
                .EndingWith("Factory")
                .BindToFactory()
                .Configure(f => f.InSingletonScope());
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.SignalR.*")
                .SelectAllClasses()
                .BindDefaultInterface();
        }
    }
}
