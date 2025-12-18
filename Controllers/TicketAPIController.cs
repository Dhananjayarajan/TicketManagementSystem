using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketProject.Application.Ticket.Create;
using TicketProject.Application.Ticket.GetAll;
using TicketProject.Application.Ticket.GetById;
using TicketProject.Application.Ticket.Update;
using TicketProject.Application.Ticket.Delete;
using TicketProject.Application.Comment;
using TicketProject.Models;

namespace TicketProject.Controllers
{
    /// <summary>
    /// Endpoints for managing helpdesk tickets
    /// </summary>
    [Route("api/tickets")]
    [ApiController]
    [Produces("application/json")]
    public class TicketAPIController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all tickets with optional filtering
        /// </summary>
        /// <param name="status">Filter by status (Open, In Progress, Resolved)</param>
        /// <param name="priority">Filter by priority (Low, Medium, High)</param>
        /// <returns>List of tickets</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] string? priority)
        {
            var tickets = await _mediator.Send(new GetAllTicketsQuery());

            if (!string.IsNullOrEmpty(status))
                tickets = tickets.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(priority))
                tickets = tickets.Where(t => t.Priority.Equals(priority, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(new ApiResponse<List<TicketDto>>
            {
                Success = true,
                Message = "Tickets retrieved successfully",
                Data = tickets
            });
        }

        /// <summary>
        /// Get a specific ticket by ID
        /// </summary>
        /// <param name="id">The ticket ID</param>
        /// <returns>Ticket details with comments</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _mediator.Send(new GetTicketByIdQuery(id));

            if (ticket == null)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Ticket not found",
                    Data = null
                });

            return Ok(new ApiResponse<TicketDetailsDto>
            {
                Success = true,
                Message = "Ticket retrieved successfully",
                Data = ticket
            });
        }

        /// <summary>
        /// Create a new ticket
        /// </summary>
        /// <param name="command">Ticket creation data</param>
        /// <returns>Created ticket ID</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Invalid ticket data",
                    Data = ModelState
                });

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, new ApiResponse<object>
            {
                Success = true,
                Message = "Ticket created successfully",
                Data = new { id }
            });
        }

        /// <summary>
        /// Update an existing ticket
        /// </summary>
        /// <param name="id">The ticket ID</param>
        /// <param name="command">Updated ticket data</param>
        /// <returns>Success status</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Invalid ticket data",
                    Data = ModelState
                });

            command.Id = id;
            var result = await _mediator.Send(command);

            if (!result)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Ticket not found",
                    Data = null
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Ticket updated successfully",
                Data = new { id }
            });
        }

        /// <summary>
        /// Update only the ticket status
        /// </summary>
        /// <param name="id">The ticket ID</param>
        /// <param name="request">New status</param>
        /// <returns>Success status</returns>
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequest request)
        {
            var ticket = await _mediator.Send(new GetTicketByIdQuery(id));

            if (ticket == null)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Ticket not found",
                    Data = null
                });

            var command = new UpdateTicketCommand
            {
                Id = id,
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority,
                Status = request.Status
            };

            await _mediator.Send(command);

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Ticket status updated successfully",
                Data = new { id, status = request.Status }
            });
        }

        /// <summary>
        /// Delete a ticket
        /// </summary>
        /// <param name="id">The ticket ID</param>
        /// <returns>Success status</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteTicketCommand(id));

            if (!result)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Ticket not found",
                    Data = null
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Ticket deleted successfully",
                Data = new { id }
            });
        }

        /// <summary>
        /// Get all comments for a ticket
        /// </summary>
        /// <param name="id">The ticket ID</param>
        /// <returns>List of comments</returns>
        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetComments(int id)
        {
            var comments = await _mediator.Send(new GetCommentsByTicketQuery(id));

            return Ok(new ApiResponse<List<CommentDto>>
            {
                Success = true,
                Message = "Comments retrieved successfully",
                Data = comments
            });
        }

        /// <summary>
        /// Add a comment to a ticket
        /// </summary>
        /// <param name="id">The ticket ID</param>
        /// <param name="request">Comment data</param>
        /// <returns>Created comment ID</returns>
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddComment(int id, [FromBody] CreateCommentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Invalid comment data",
                    Data = ModelState
                });

            var command = new CreateCommentCommand
            {
                TicketId = id,
                Text = request.Text,
                Author = request.Author
            };

            var commentId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetComments), new { id }, new ApiResponse<object>
            {
                Success = true,
                Message = "Comment added successfully",
                Data = new { commentId }
            });
        }
    }
}