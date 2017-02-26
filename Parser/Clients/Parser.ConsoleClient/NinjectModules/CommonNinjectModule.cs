using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Modules;

namespace Parser.ConsoleClient.NinjectModules
{
    internal class CommonNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.Common.*")
                .SelectAllClasses()
                .BindDefaultInterface();
        }
    }
}
