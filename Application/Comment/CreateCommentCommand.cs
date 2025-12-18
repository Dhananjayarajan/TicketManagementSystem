using MediatR;

public class CreateCommentCommand : IRequest<int>
{
	public int TicketId { get; set; }
	public required string Text { get; set; }
	public required string Author { get; set; }
}