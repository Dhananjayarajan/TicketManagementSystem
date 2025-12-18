using System;
using MediatR;

namespace TicketProject.Application.Ticket.Create;

public class CreateTicketCommand : IRequest<int>
{
	public required string Title { get; set; }
	public required string Description { get; set; }
	public string Priority { get; set; } = "Medium";


}
