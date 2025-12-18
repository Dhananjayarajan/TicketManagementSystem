using System;

namespace TicketProject.Application.Ticket.GetAll;

public class TicketDto
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public string Status { get; set; } = "Open";
	public string Priority { get; set; } = "Medium";

	public DateTime CreatedDate { get; set; } =
	DateTime.Now;
}
