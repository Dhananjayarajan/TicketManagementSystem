using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketProject.Application.Ticket.Create;
using TicketProject.Application.Ticket.GetAll;
using TicketProject.Application.Ticket.GetById;
using TicketProject.Application.Ticket.Update;
using TicketProject.Application.Ticket.Delete;
using TicketProject.Application.Comment;

namespace TicketProject.Controllers
{
    public class TicketController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TicketController> _logger;

        public TicketController(IMediator mediator, ILogger<TicketController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: Ticket/Index
        public async Task<IActionResult> Index()
        {
            var tickets = await _mediator.Send(new GetAllTicketsQuery());
            return View(tickets);
        }

        // GET: Ticket/Details/5
        public async Task<IActionResult> Details(int id)
        {
            _logger.LogInformation("Loading ticket details for ID: {TicketId}", id);

            var ticket = await _mediator.Send(new GetTicketByIdQuery(id));

            if (ticket == null)
            {
                _logger.LogWarning("Ticket not found with ID: {TicketId}", id);
                return NotFound();
            }

            _logger.LogInformation("Ticket {TicketId} loaded with {CommentCount} comments",
                id, ticket.Comments?.Count ?? 0);

            return View(ticket);
        }

        // GET: Ticket/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTicketCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            var id = await _mediator.Send(command);
            TempData["Success"] = "Ticket created successfully!";
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: Ticket/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _mediator.Send(new GetTicketByIdQuery(id));

            if (ticket == null)
                return NotFound();

            var command = new UpdateTicketCommand
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                Priority = ticket.Priority
            };

            return View(command);
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateTicketCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(command);

            var result = await _mediator.Send(command);

            if (!result)
                return NotFound();

            TempData["Success"] = "Ticket updated successfully!";
            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: Ticket/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteTicketCommand(id));

            if (!result)
                return NotFound();

            TempData["Success"] = "Ticket deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        // POST: Ticket/AddComment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int ticketId, string text, string author)
        {
            _logger.LogInformation("Adding comment to ticket {TicketId} by {Author}", ticketId, author);

            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(author))
            {
                TempData["Error"] = "Comment text and author are required.";
                return RedirectToAction(nameof(Details), new { id = ticketId });
            }

            var command = new CreateCommentCommand
            {
                TicketId = ticketId,
                Text = text,
                Author = author
            };

            var commentId = await _mediator.Send(command);

            _logger.LogInformation("Comment {CommentId} added to ticket {TicketId}", commentId, ticketId);

            TempData["Success"] = "Comment added successfully!";
            TempData["CommentAdded"] = true;

            return RedirectToAction(nameof(Details), new { id = ticketId });
        }

        // POST: Ticket/DeleteComment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int commentId, int ticketId)
        {
            _logger.LogInformation("Deleting comment {CommentId} from ticket {TicketId}", commentId, ticketId);

            var result = await _mediator.Send(new DeleteCommentCommand(commentId));

            if (result)
            {
                TempData["Success"] = "Comment deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Comment not found.";
            }

            return RedirectToAction(nameof(Details), new { id = ticketId });
        }
    }
}