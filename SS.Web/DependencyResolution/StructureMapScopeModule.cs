using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SS.Web.App_Start;
using StructureMap.Web.Pipeline;

namespace SS.Web.DependencyResolution
{
    public class StructureMapScopeModule : IHttpModule
    {
        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) => StructuremapMvc.ParentScope.CreateNestedContainer();
            context.EndRequest += (sender, e) =>
            {
                HttpContextLifecycle.DisposeAndClearAll();
                StructuremapMvc.ParentScope.DisposeNestedContainer();
            };
        }

        #endregion
    }
}