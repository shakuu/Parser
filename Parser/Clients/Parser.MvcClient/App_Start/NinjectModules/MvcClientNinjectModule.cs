using System.Linq;
using System.Reflection;
using System.Web.Mvc;

using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Interception.Infrastructure.Language;
using Ninject.Modules;

using Parser.Common.Interceptors;
using Parser.MvcClient.App_Start.ControllerFactories;

namespace Parser.MvcClient.App_Start.NinjectModules
{
    public class MvcClientNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);

            this.Bind<IControllerFactory>().To<ParserControllerFactory>().InSingletonScope();
            this.Bind<INinjectControllerFactory>().ToFactory().InSingletonScope();
            this.Bind<IController>().ToMethod(this.NinjectControllerFactoryMethod).NamedLikeFactoryMethod((INinjectControllerFactory factory) => factory.GetController(null));
        }

        private IController NinjectControllerFactoryMethod(IContext context)
        {
            var controllerName = (string)context.Parameters.FirstOrDefault()?.GetValue(context, null);

            var controllerType = Assembly.GetExecutingAssembly().GetExportedTypes().FirstOrDefault(t => t.Name.Contains(controllerName));

            return (IController)context.Kernel.Get(controllerType);
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