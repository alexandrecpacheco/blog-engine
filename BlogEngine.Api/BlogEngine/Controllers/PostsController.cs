using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain.Intefaces.Data.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService) 
        { 
            _postsService = postsService;
        }

        [Attributes.Authorize(Role.Public, Role.Writer, Role.Editor)]
        [HttpGet("get-posts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPosts()
        {
            if (!ModelState.IsValid) return BadRequest();

            var search = await _postsService.GetPostsAsync();

            return await ResponseResult(search);
        }

        [Attributes.Authorize(Role.Public, Role.Writer, Role.Editor)]
        [HttpGet("get-published-posts-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPublishedPostById([FromQuery] int postId)
        {
            if (!ModelState.IsValid) return BadRequest();

            var published = await _postsService.GetPublishedPostByIdAsync(postId);
            var result = new { published };
            
            return await ResponseResult(result);
        }

        [Attributes.Authorize(Role.Writer)]
        [HttpPost("create-post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(PostRequest request)
        {
            if (request == null) return BadRequest();
            
            await _postsService.CreateAsync(request);

            return await ResponseResult(true);
        }
    }
}
