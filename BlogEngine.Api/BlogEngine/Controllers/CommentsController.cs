using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain.Intefaces.Data.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [Attributes.Authorize(Role.Public, Role.Writer, Role.Editor)]
        [HttpPost("create-comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(CommentRequest request)
        {
            if (request == null) return BadRequest();

            await _commentsService.CreateAsync(request);

            return await ResponseResult(true);
        }
    }
}
