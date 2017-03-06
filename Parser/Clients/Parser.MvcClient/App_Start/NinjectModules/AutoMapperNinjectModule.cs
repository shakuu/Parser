using AutoMapper;

using Ninject.Activation;
using Ninject.Modules;

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
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                //cfg.AddProfile();
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}