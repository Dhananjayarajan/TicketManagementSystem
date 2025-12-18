using System;

namespace TicketProject.Domain;

public class TicketEntity
{
	public int Id { get; set; }
	public required string Title { get; set; }
	public required string Description { get; set; }
	public string Status { get; set; } = "Open";
	public string Priority { get; set; } = "Medium";

	public ICollection<Comment>? Comments { get; set; }
	public DateTime CreatedDate { get; set; } =
	DateTime.Now;
}
