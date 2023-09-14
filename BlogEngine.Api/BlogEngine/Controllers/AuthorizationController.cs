using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BlogEngine.Domain.Intefaces.Data.Service;

namespace BlogEngine.Controllers
{
    [Route("api/[controller]")]
    public class AuthorizationController : BaseController
    {
        private readonly IAuthorService _authorService;
        private readonly ApiSettings _apiSettings;

        public AuthorizationController(IAuthorService authorService, ApiSettings apiSettings)
        {
            _authorService = authorService;
            _apiSettings = apiSettings;
        }

        [HttpPost("authentication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> Authentication([FromBody] SignInRequest request)
        {
            if (request == null) return BadRequest();

            var response = await _authorService.AuthenticationAsync(request);
            if (response == null) return await ResponseResult(false);
            var userAuthenticatedResponse = GenerateToken(response, _apiSettings);

            return await ResponseResult(userAuthenticatedResponse);
        }
    }
}
