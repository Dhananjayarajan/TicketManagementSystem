using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketProject.Application.Ticket.Create;
using TicketProject.Application.Ticket.GetAll;

namespace TicketProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketAPIController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TicketAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id }, new { id });
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllTicketsQuery()));
        }


    }
}
