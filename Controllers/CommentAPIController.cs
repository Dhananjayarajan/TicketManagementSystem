using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketProject.Application.Comment;
using TicketProject.Models;

namespace TicketProject.Controllers
{
    /// <summary>
    /// Endpoints for managing comments
    /// </summary>
    [Route("api/comments")]
    [ApiController]
    [Produces("application/json")]
    public class CommentAPIController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Delete a comment
        /// </summary>
        /// <param name="id">The comment ID</param>
        /// <returns>Success status</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCommentCommand(id));

            if (!result)
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Comment not found",
                    Data = null
                });

            return Ok(new ApiResponse<object>
            {
                Success = true,
                Message = "Comment deleted successfully",
                Data = new { id }
            });
        }
    }
}