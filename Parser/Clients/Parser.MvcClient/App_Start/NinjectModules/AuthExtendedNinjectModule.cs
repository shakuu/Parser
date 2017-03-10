using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Modules;

namespace Parser.MvcClient.App_Start.NinjectModules
{
    public class AuthExtendedNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.Auth.Extended.*")
                .SelectAllClasses()
                .BindDefaultInterface();
        }
    }
}