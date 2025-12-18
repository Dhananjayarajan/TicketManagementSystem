using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketProject.Persistance;

namespace TicketProject.Application.Ticket.GetAll;

public class GetAllTickets : IRequestHandler<GetAllTicketsQuery, List<TicketDto>>
{
	private readonly AppDbContext _context;

	public GetAllTickets(AppDbContext context)
	{
		_context = context;
	}

	public async Task<List<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
	{
		return await _context.Tickets.Select(x => new TicketDto
		{
			Id = x.Id,
			Title = x.Title,
			Status = x.Status,
			Priority = x.Priority,
			CreatedDate = x.CreatedDate
		}).ToListAsync(cancellationToken);
	}
}
