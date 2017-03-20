using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;

using Parser.Common.Interceptors;
using Parser.MvcClient.Controllers;

namespace Parser.MvcClient.App_Start.NinjectModules
{
    public class MvcClientNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);

            this.Rebind<HomeController>().ToSelf().Intercept().With<LoggingInterceptor>();
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.MvcClient.*")
                .SelectAllClasses()
                .EndingWith("Controller")
                .BindToSelf()
                .Configure(c => c.Intercept().With<LoggingInterceptor>());
        }
    }
}