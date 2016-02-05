using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Mvc;
using SS.Web.DAL;
using SS.Web.Infrastructure;
using SS.Web.Infrastructure.Validation;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.Pipeline;

namespace SS.Web.DependencyResolution
{
    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.LookForRegistries();
                    scan.AssemblyContainingType<DefaultRegistry>();
                    scan.AddAllTypesOf(typeof(IModelBinder));
                    scan.AddAllTypesOf(typeof(IModelBinderProvider));
                    scan.With(new ControllerConvention());
                });
            For<ClassContext>().Use(() => new ClassContext("ClassContext")).LifecycleIs<TransientLifecycle>();
            For<IControllerFactory>().Use<ControllerFactory>();
            For<ModelValidatorProvider>().Use<FluentValidationModelValidatorProvider>();
            For<IValidatorFactory>().Use<StructureMapValidatorFactory>();
        }

        #endregion
    }
}