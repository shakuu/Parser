using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;

using Parser.Common.Interceptors;
using Parser.Data.Services;

namespace Parser.MvcClient.App_Start.NinjectModules
{
    public class DataServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);
            this.Bind(this.BindFactoriesByConvention);

            this.Bind<IInterceptor>().To<ParameterizedCachingInterceptor>().WhenInjectedExactlyInto<ManagedCachingInterceptor>();

            //Kernel.InterceptReplace<CombatStatisticsPersistentStorageStrategy>(s => s.StoreCombatStatistics(null), this.InterceptedStoreCombatStatisticsMethod);
        }

        private void InterceptedStoreCombatStatisticsMethod(IInvocation invocation)
        {
            // Do nothing for testing.
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.Data.Services.*")
                .SelectAllClasses()
                .BindDefaultInterface()
                .ConfigureFor<LeaderboardDamageService>(c => c.Intercept().With<ManagedCachingInterceptor>())
                .ConfigureFor<LeaderboardHealingService>(c => c.Intercept().With<ManagedCachingInterceptor>())
                .ConfigureFor<LeaderboardService>(c => c.Intercept().With<ManagedCachingInterceptor>());
        }

        private void BindFactoriesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.Data.Services.*")
                .SelectAllInterfaces()
                .EndingWith("Factory")
                .BindToFactory()
                .Configure(f => f.InSingletonScope());
        }
    }
}