using System;

namespace TicketProject.Domain;

public class Comment
{
	public int Id { get; set; }
	public string? Text { get; set; }
	public string? Author { get; set; }
	public int TicketId { get; set; }
	public DateTime CreatedDate { get; set; } =
	DateTime.Now;
}
