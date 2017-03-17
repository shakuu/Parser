﻿using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Modules;

using Parser.Common.Html.Svg;

namespace Parser.MvcClient.App_Start.NinjectModules
{
    public class CommonNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(this.BindAllClassesByConvention);
            this.Bind(this.BindFactoriesByConvention);
        }

        private void BindAllClassesByConvention(IFromSyntax bind)
        {
            bind
                .FromAssembliesMatching("*.Common.*")
                .SelectAllClasses()
                .BindDefaultInterface()
                .ConfigureFor<ProgressPartialCircleSvgPathStringProvider>(c => c.InSingletonScope());
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