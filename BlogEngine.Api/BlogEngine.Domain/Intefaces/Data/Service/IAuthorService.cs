using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain.Entities;

namespace BlogEngine.Domain.Intefaces.Data.Service
{
    public interface IAuthorService
    {
        Task<AuthorEntity> AuthenticationAsync(SignInRequest signInRequest);
        Task CreateAsync(AuthorRequest userRequest);
        Task<AuthorEntity> GetByEmailAsync(string email);
    }
}
