using System;
using MediatR;
using TicketProject.Domain;
using TicketProject.Persistance;

namespace TicketProject.Application.Ticket.Create;

public class CreateTicketHandler : IRequestHandler<CreateTicketCommand, int>
{
	private readonly AppDbContext _context;

	public CreateTicketHandler(AppDbContext context)
	{
		_context = context;
	}

	public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
	{
		var ticket = new TicketEntity
		{
			Title = request.Title,
			Description = request.Description,
			Priority = request.Priority
		};

		_context.Tickets.Add(ticket);
		await _context.SaveChangesAsync(cancellationToken);

		return ticket.Id;
	}
}
