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
        [HttpGet("search-posts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPosts()
        {
            if (!ModelState.IsValid) return BadRequest();

            var search = await _postsService.GetPostsAsync();

            return await ResponseResult(search);
        }
    }
}
