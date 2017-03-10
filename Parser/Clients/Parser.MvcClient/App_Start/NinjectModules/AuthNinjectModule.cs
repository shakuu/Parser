using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Modules;

using Parser.Auth.Contracts;
using Parser.Auth.Extended.Contracts;

using Parser.MvcClient.Controllers;

namespace Parser.MvcClient.App_Start.NinjectModules
{
    public class AuthNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);
            this.Bind(this.BindFactoriesByConvention);

            this.Bind<IIdentityAuthAccountService>().ToMethod(this.IIdentityAuthAccountServiceFactoryMethod).WhenInjectedExactlyInto<AccountController>();
        }

        private IIdentityAuthAccountService IIdentityAuthAccountServiceFactoryMethod(IContext context)
        {
            var extendedIdentityAuthAccountService = context.Kernel.Get<IExtendedIdentityAuthAccountService>();

            return extendedIdentityAuthAccountService;
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