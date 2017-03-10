using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Modules;

namespace Parser.MvcClient.App_Start.NinjectModules
{
    public class AuthNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);
            this.Bind(this.BindFactoriesByConvention);

            //this.Rebind(typeof(IDbContext), typeof(IParserDbContext)).To<ParserDbContext>().InRequestScope();
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.Auth.*")
                .SelectAllClasses()
                .BindDefaultInterface();
        }

        private void BindFactoriesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.Auth.*")
                .SelectAllInterfaces()
                .EndingWith("Factory")
                .BindToFactory()
                .Configure(f => f.InSingletonScope());
        }
    }
}