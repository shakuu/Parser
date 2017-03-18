using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Modules;

using Parser.LogFile.SignalR.Services;

namespace Parser.MvcClient.App_Start.NinjectModules
{
    public class SignalRNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.SignalR.*")
                .SelectAllClasses()
                .BindDefaultInterface()
                .ConfigureFor<LogFileParserHubService>(s => s.InSingletonScope());
        }
    }
}