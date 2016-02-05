using System.Web.Mvc;
using MediatR;

namespace SS.Web.Features.Student
{
    public class UiController : Controller
    {
        private readonly IMediator _mediator;

        public UiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ViewResult Index(Index.Query query)
        {
            var model = _mediator.Send(query);

            return View(model);
        }

    }
}