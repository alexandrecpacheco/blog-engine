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

        /// <summary>
        /// Get published posts with approval status
        /// </summary>
        /// <returns>List of posts approved</returns>
        [Attributes.Authorize(Role.Public, Role.Writer, Role.Editor)]
        [HttpGet("get-published-posts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPublishedPosts()
        {
            if (!ModelState.IsValid) return BadRequest();

            var search = await _postsService.GetPublishedPostsAsync();

            return await ResponseResult(search);
        }

        /// <summary>
        /// Get pulished post by id
        /// </summary>
        /// <param name="postId">postId</param>
        /// <returns>Published Post</returns>
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

        /// <summary>
        /// Create posts only by Writers
        /// </summary>
        /// <param name="request">PosRequest</param>
        /// <returns>Ok</returns>
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

        /// <summary>
        /// Update Posts only by Writer if only its Rejected
        /// </summary>
        /// <param name="request">PostUpdateRequest</param>
        /// <returns>Ok</returns>
        [Attributes.Authorize(Role.Writer)]
        [HttpPut("update-post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(PostUpdateRequest request)
        {
            if (request == null) return BadRequest();

            await _postsService.UpdateAsync(request);

            return await ResponseResult(true);
        }

        /// <summary>
        /// Get Pending Posts only by Editor
        /// </summary>
        /// <returns></returns>
        [Attributes.Authorize(Role.Editor)]
        [HttpGet("get-pending-posts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPendingPosts()
        {
            if (!ModelState.IsValid) return BadRequest();

            var search = await _postsService.GetPendingPostsAsync();

            return await ResponseResult(search);
        }
    }
}
