using AutoMapper;

using Ninject.Activation;
using Ninject.Modules;

using Parser.MvcClient.App_Start.AutoMapperProfiles;

namespace Parser.MvcClient.App_Start.NinjectModules
{
    public class AutoMapperNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMapper>().ToMethod(this.IMapperFactoryMethod).InSingletonScope();
        }

        private IMapper IMapperFactoryMethod(IContext context)
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile<ParserUserProfile>();
                configuration.AddProfile<StoredCombatStatisticsProfile>();
            });

            mapperConfiguration.AssertConfigurationIsValid();

            return mapperConfiguration.CreateMapper();
        }
    }
}