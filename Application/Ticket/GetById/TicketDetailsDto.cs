using TicketProject.Models;

namespace TicketProject.Application.Ticket.GetById;

public class TicketDetailsDto
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public required string Description { get; set; }
	public string Status { get; set; } = "Open";
	public string Priority { get; set; } = "Medium";
	public DateTime CreatedDate { get; set; }
	public List<CommentDto> Comments { get; set; } = new();
}
