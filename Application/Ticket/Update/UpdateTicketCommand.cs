using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TicketProject.Application.Ticket.Update;

/// <summary>
/// Command to update an existing ticket
/// </summary>
public class UpdateTicketCommand : IRequest<bool>
{
	/// <summary>
	/// Ticket ID (set automatically from route)
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Updated ticket title (3-100 characters)
	/// </summary>
	[Required]
	[StringLength(100, MinimumLength = 3)]
	public required string Title { get; set; }

	/// <summary>
	/// Updated description (10-500 characters)
	/// </summary>
	[Required]
	[StringLength(500, MinimumLength = 10)]
	public required string Description { get; set; }

	/// <summary>
	/// Status: Open, In Progress, or Resolved
	/// </summary>
	[Required]
	public string Status { get; set; } = "Open";

	/// <summary>
	/// Priority: Low, Medium, or High
	/// </summary>
	[Required]
	public string Priority { get; set; } = "Medium";
}