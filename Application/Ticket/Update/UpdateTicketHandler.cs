using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketProject.Persistance;

namespace TicketProject.Application.Ticket.Update;

public class UpdateTicketHandler : IRequestHandler<UpdateTicketCommand, bool>
{
	private readonly AppDbContext _context;

	public UpdateTicketHandler(AppDbContext context)
	{
		_context = context;
	}

	public async Task<bool> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
	{
		var ticket = await _context.Tickets
				.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

		if (ticket == null)
			return false;

		ticket.Title = request.Title;
		ticket.Description = request.Description;
		ticket.Status = request.Status;
		ticket.Priority = request.Priority;

		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}
}