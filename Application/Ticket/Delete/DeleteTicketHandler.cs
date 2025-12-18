using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketProject.Persistance;

namespace TicketProject.Application.Ticket.Delete;

public class DeleteTicketHandler : IRequestHandler<DeleteTicketCommand, bool>
{
	private readonly AppDbContext _context;

	public DeleteTicketHandler(AppDbContext context)
	{
		_context = context;
	}

	public async Task<bool> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
	{
		var ticket = await _context.Tickets
				.Include(t => t.Comments)
				.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

		if (ticket == null)
			return false;

		_context.Tickets.Remove(ticket);
		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}
}