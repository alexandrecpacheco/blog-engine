using BlogEngine.Domain.Entities;
using System.Data.Common;

namespace BlogEngine.Domain.Intefaces.Data.Repository
{
    public interface IAuthorProfileRepository
    {
        Task<AuthorProfile> GetUserProfileAsync(string name, string email);
        Task<int> CreateAsync(AuthorProfile userProfile, DbConnection dbConnection, DbTransaction dbTransaction);
    }
}
