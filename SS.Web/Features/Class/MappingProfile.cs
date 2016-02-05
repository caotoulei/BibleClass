using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace SS.Web.Features.Class
{
    using AutoMapper;
    public class MappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Create.Command, Models.Class>();
        }
    }
}