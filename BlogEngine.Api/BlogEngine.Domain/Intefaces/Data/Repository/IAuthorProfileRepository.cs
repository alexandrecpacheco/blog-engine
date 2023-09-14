using BlogEngine.Domain.Entities;
using System.Data.Common;

namespace BlogEngine.Domain.Intefaces.Data.Repository
{
    public interface IAuthorProfileRepository
    {
        Task<AuthorProfileEntity> GetUserProfileAsync(string name, string email);
        Task<int> CreateAsync(AuthorProfileEntity userProfile, DbConnection dbConnection, DbTransaction dbTransaction);
    }
}
