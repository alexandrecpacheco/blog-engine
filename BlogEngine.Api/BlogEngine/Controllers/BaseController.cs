using BlogEngine.Domain.Utils;
using BlogEngine.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BlogEngine.Domain.Dto.Response;
using BlogEngine.Domain.Entities;
using BlogEngine.Security;

namespace BlogEngine.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {
        protected Task<IActionResult> ResponseResult(object response)
        {
            try
            {
                IActionResult ok = Ok(new { success = true, data = response });
                return Task.FromResult(ok);
            }
            catch (Exception e)
            {
                return Task.FromResult<IActionResult>(BadRequest(new
                { success = false, errors = new[] { e.InnerException } }));
            }
        }

        protected static AuthorAuthenticatedResponse GenerateToken(AuthorEntity author, ApiSettings apiSettings)
        {
            var claims = author.AuthorProfile.Profile.Description;

            var tokenBuilder = new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Create(apiSettings.Values.SecurityKey))
                .AddSubject("Authentication")
                .AddIssuer(apiSettings.Values.Issuer)
                .AddAudience(apiSettings.Values.Audience)
                .AddClaim("name", author.Name)
                .AddClaim("id", author.AuthorId.ToString())
                .AddClaimRole("role", string.Join(",", claims))
                .AddAlgorithm(SecurityAlgorithms.HmacSha256)
                .AddExpiry(apiSettings.Values.ExpirationTime);

            var token = tokenBuilder.Build();
            var refreshToken = Cryptography.SHA256(Guid.NewGuid().ToString());
            var userAuthenticatedResponse = new AuthorAuthenticatedResponse
            {
                Token = token.Value,
                RefreshToken = refreshToken,
                Name = author.Name,
                Email = author.Email
            };

            return userAuthenticatedResponse;
        }
    }
}
