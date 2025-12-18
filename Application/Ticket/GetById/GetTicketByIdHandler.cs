using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TicketProject.Persistance;
using TicketProject.Models;

namespace TicketProject.Application.Ticket.GetById;

public class GetTicketByIdHandler : IRequestHandler<GetTicketByIdQuery, TicketDetailsDto?>
{
	private readonly AppDbContext _context;
	private readonly ILogger<GetTicketByIdHandler> _logger;

	public GetTicketByIdHandler(AppDbContext context, ILogger<GetTicketByIdHandler> logger)
	{
		_context = context;
		_logger = logger;
	}

	public async Task<TicketDetailsDto?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Fetching ticket with ID: {TicketId}", request.Id);

		var ticket = await _context.Tickets
				.Include(t => t.Comments)
				.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

		if (ticket == null)
		{
			_logger.LogWarning("Ticket with ID {TicketId} not found", request.Id);
			return null;
		}

		_logger.LogInformation("Ticket loaded. Comments collection is null: {IsNull}, Count: {Count}",
				ticket.Comments == null,
				ticket.Comments?.Count ?? 0);

		// Debug: Log each comment
		if (ticket.Comments != null)
		{
			foreach (var c in ticket.Comments)
			{
				_logger.LogInformation("Comment ID: {CommentId}, Text: {Text}, Author: {Author}",
						c.Id, c.Text, c.Author);
			}
		}

		var result = new TicketDetailsDto
		{
			Id = ticket.Id,
			Title = ticket.Title,
			Description = ticket.Description,
			Status = ticket.Status,
			Priority = ticket.Priority,
			CreatedDate = ticket.CreatedDate,
			Comments = ticket.Comments?.Select(c => new CommentDto
			{
				Id = c.Id,
				Text = c.Text,
				Author = c.Author,
				CreatedDate = c.CreatedDate
			}).OrderByDescending(c => c.CreatedDate).ToList() ?? new List<CommentDto>()
		};

		_logger.LogInformation("Returning DTO with {CommentCount} comments", result.Comments.Count);

		return result;
	}
}