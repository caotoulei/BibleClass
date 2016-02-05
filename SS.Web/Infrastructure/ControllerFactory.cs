using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SS.Web.Infrastructure
{
    public class ControllerFactory : DefaultControllerFactory
    {
        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            return
                typeof(ControllerFactory).Assembly.GetType("SS.Web.Features." + controllerName +
                                                            ".UiController");
        }
    }
}