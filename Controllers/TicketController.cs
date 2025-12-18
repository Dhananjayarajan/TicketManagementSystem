using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketProject.Application.Ticket.Create;
using TicketProject.Application.Ticket.GetAll;

namespace TicketProject.Controllers
{

    public class TicketController : Controller
    {
        private readonly IMediator _mediator;
        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _mediator.Send(new GetAllTicketsQuery());
            return View(tickets);
        }


    }
}
