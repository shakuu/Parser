using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;

using Parser.Auth.Remote;
using Parser.Auth.Remote.Models;
using Parser.Auth.Remote.Services;

namespace Parser.ConsoleClient.NinjectModules
{
    public class AuthRemoteNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);
            this.Bind(this.BindFactoriesByConvention);

            this.Bind(typeof(IRemoteUserProvider), typeof(IRemoteUserLoginService)).To<RemoteUserService>().InSingletonScope();

            // TODO: Delete
            this.Kernel.InterceptReplace<RemoteUserService>(s => s.GetLoggedInRemoteUser(), this.GetLoggedInRemoteUserInterceptMethod);
        }

        private void GetLoggedInRemoteUserInterceptMethod(IInvocation invocation)
        {
            invocation.ReturnValue = new RemoteUser("myuser@user.com");
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.Auth.Remote.*")
                .SelectAllClasses()
                .BindDefaultInterface()
                .ConfigureFor<RemoteUserService>(s => s.InSingletonScope());
        }

        private void BindFactoriesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.Auth.Remote.*")
                .SelectAllInterfaces()
                .EndingWith("Factory")
                .BindToFactory()
                .Configure(f => f.InSingletonScope());
        }
    }
}