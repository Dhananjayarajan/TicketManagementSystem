using System.ComponentModel.DataAnnotations;

namespace TicketProject.Models;

/// <summary>
/// Shared API Response wrapper for all API endpoints
/// </summary>
public class ApiResponse<T>
{
	/// <summary>
	/// Indicates if the operation was successful
	/// </summary>
	public bool Success { get; set; }

	/// <summary>
	/// Human-readable message about the operation result
	/// </summary>
	public string Message { get; set; } = string.Empty;

	/// <summary>
	/// The actual data payload
	/// </summary>
	public T? Data { get; set; }
}

/// <summary>
/// Request model for updating ticket status via PATCH
/// </summary>
public class UpdateStatusRequest
{
	/// <summary>
	/// New status for the ticket (Open, In Progress, Resolved)
	/// </summary>
	[Required]
	public required string Status { get; set; }
}

/// <summary>
/// Request model for creating comments via API
/// </summary>
public class CreateCommentRequest
{
	/// <summary>
	/// The comment text content
	/// </summary>
	[Required]
	[StringLength(1000, MinimumLength = 1)]
	public required string Text { get; set; }

	/// <summary>
	/// Name of the person making the comment
	/// </summary>
	[Required]
	[StringLength(100, MinimumLength = 1)]
	public required string Author { get; set; }
}

/// <summary>
/// Shared DTO for comment data across all layers
/// </summary>
public class CommentDto
{
	/// <summary>
	/// Unique identifier for the comment
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// The comment text
	/// </summary>
	public string? Text { get; set; }

	/// <summary>
	/// Author of the comment
	/// </summary>
	public string? Author { get; set; }

	/// <summary>
	/// When the comment was created
	/// </summary>
	public DateTime CreatedDate { get; set; }
}