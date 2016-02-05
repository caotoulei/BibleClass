using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MediatR;
using SS.Web.Infrastructure;

namespace SS.Web.Features.Class
{
    public class UiController : Controller
    {
        private readonly IMediator _mediator;
        public UiController(IMediator mediator)
        {
            _mediator = mediator;
        }
       
        public ActionResult Create()
        {
            var command = new Create.Command();
            return View(command);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Create.Command command)
        {
            _mediator.Send(command);
            return View(command);
            //return this.RedirectToActionJson("Index");
        }

    }
}