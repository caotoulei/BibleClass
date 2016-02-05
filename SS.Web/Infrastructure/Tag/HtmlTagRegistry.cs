using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlTags.Conventions;
using StructureMap.Configuration.DSL;

namespace SS.Web.Infrastructure.Tag
{
    public class HtmlTagRegistry : Registry
    {

        public HtmlTagRegistry()
        {

            var htmlConventionLibrary = new HtmlConventionLibrary();
            new DefaultAspNetMvcHtmlConventions().Apply(htmlConventionLibrary);
            new DefaultHtmlConventions().Apply(htmlConventionLibrary);
            For<HtmlConventionLibrary>().Use(htmlConventionLibrary);
        }
    }
}