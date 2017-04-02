using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Modules;
using Parser.LogFile.SignalR.Contracts;
using Parser.LogFile.SignalR.Services;
using Parser.MvcClient.SignalRHubs;

namespace Parser.MvcClient.App_Start.NinjectModules
{
    public class SignalRNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);

            this.Bind<ILogFileParserHub>().To<LogFileParserHub>();
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