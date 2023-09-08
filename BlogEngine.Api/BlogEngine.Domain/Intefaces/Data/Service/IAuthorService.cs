using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain.Entities;

namespace BlogEngine.Domain.Intefaces.Data.Service
{
    public interface IAuthorService
    {
        Task<Author> AuthenticationAsync(SignInRequest signInRequest);
        Task CreateAsync(AuthorRequest userRequest);
        Task<Author> GetByEmailAsync(string email);
    }
}
