using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;

using Parser.Common.Html.Svg;
using Parser.Common.Providers;
using Parser.Common.Utilities.Providers;

namespace Parser.MvcClient.App_Start.NinjectModules
{
    public class CommonNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);
            this.Bind(this.BindFactoriesByConvention);

            //this.Kernel.InterceptReplace<IdentityProvider>(p => p.GetUsername(), this.GetUsernameTestingInterceptor);
        }

        private void GetUsernameTestingInterceptor(IInvocation invocation)
        {
            invocation.ReturnValue = "myuser@user.com";
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.Common.*")
                .SelectAllClasses()
                .BindDefaultInterface()
                .ConfigureFor<PartialCircleSvgPathProvider>(c => c.InSingletonScope())
                .ConfigureFor<StartupTimestampProvider>(c => c.InSingletonScope());
        }

        private void BindFactoriesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.Common.*")
                .SelectAllInterfaces()
                .EndingWith("Factory")
                .BindToFactory()
                .Configure(f => f.InSingletonScope());
        }
    }
}