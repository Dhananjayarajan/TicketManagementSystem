using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketProject.Application.Ticket.GetById;
using TicketProject.Models;
using TicketProject.Persistance;

namespace TicketProject.Application.Comment;


public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, int>
{
	private readonly AppDbContext _context;

	public CreateCommentHandler(AppDbContext context)
	{
		_context = context;
	}

	public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
	{
		var comment = new Domain.Comment
		{
			TicketId = request.TicketId,
			Text = request.Text,
			Author = request.Author,
			CreatedDate = DateTime.Now
		};

		_context.Comments.Add(comment);
		await _context.SaveChangesAsync(cancellationToken);

		return comment.Id;
	}
}

// Get Comments by Ticket
public record GetCommentsByTicketQuery(int TicketId) : IRequest<List<CommentDto>>;


public class GetCommentsByTicketHandler : IRequestHandler<GetCommentsByTicketQuery, List<CommentDto>>
{
	private readonly AppDbContext _context;

	public GetCommentsByTicketHandler(AppDbContext context)
	{
		_context = context;
	}

	public async Task<List<CommentDto>> Handle(GetCommentsByTicketQuery request, CancellationToken cancellationToken)
	{
		return await _context.Comments
				.Where(c => c.TicketId == request.TicketId)
				.OrderByDescending(c => c.CreatedDate)
				.Select(c => new CommentDto
				{
					Id = c.Id,
					Text = c.Text,
					Author = c.Author,
					CreatedDate = c.CreatedDate
				})
				.ToListAsync(cancellationToken);
	}
}

// Delete Comment
public record DeleteCommentCommand(int Id) : IRequest<bool>;

public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, bool>
{
	private readonly AppDbContext _context;

	public DeleteCommentHandler(AppDbContext context)
	{
		_context = context;
	}

	public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
	{
		var comment = await _context.Comments
				.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

		if (comment == null)
			return false;

		_context.Comments.Remove(comment);
		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}
}