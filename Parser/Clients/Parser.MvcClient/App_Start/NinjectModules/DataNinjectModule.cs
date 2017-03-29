using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Modules;
using Ninject.Web.Common;

using Parser.Data;
using Parser.Data.Contracts;

namespace Parser.MvcClient.App_Start.NinjectModules
{
    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);
            this.Bind(this.BindFactoriesByConvention);

            this.Rebind(typeof(IDbContext), typeof(IParserDbContext)).To<ParserDbContext>().InRequestScope();
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("Parser.Data.dll")
                .SelectAllClasses()
                .BindDefaultInterface()
                .ConfigureFor<ParserDbContext>(c => c.InRequestScope());
        }

        private void BindFactoriesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("Parser.Data.dll")
                .SelectAllInterfaces()
                .EndingWith("Factory")
                .BindToFactory()
                .Configure(f => f.InSingletonScope());
        }
    }
}