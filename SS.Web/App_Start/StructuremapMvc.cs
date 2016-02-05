using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using SS.Web.App_Start;
using SS.Web.DependencyResolution;
using SS.Web.Infrastructure.Mapping;
using StructureMap;


using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(StructuremapMvc), "Start")]
[assembly: ApplicationShutdownMethod(typeof(StructuremapMvc), "End")]

namespace SS.Web.App_Start
{
    public static class StructuremapMvc
    {
        public static StructureMapDependencyScope ParentScope { get; set; }

        public static void End()
        {
            ParentScope.Dispose();
            ParentScope.DisposeParentContainer();
        }

        public static void Start()
        {
            IContainer container = IoC.Initialize();
            ParentScope = new StructureMapDependencyScope(container, new HttpContextNestedContainerScope());
            DependencyResolver.SetResolver(ParentScope);
            DynamicModuleUtility.RegisterModule(typeof(StructureMapScopeModule));
            AutoMapperBootstrapper.Initialize(ParentScope.Container);
        }
    }

    public class HttpContextNestedContainerScope : INestedContainerScope
    {
        private const string NestedContainerKey = "Nested.Container.Key";

        public IContainer NestedContainer
        {
            get { return (IContainer)(HttpContext.Current != null ? HttpContext.Current.Items[NestedContainerKey] : null); }
            set { HttpContext.Current.Items[NestedContainerKey] = value; }
        }
    }
}