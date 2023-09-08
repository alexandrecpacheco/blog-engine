using BlogEngine.Domain.Entities;
using System.Data.Common;

namespace BlogEngine.Domain.Intefaces.Data.Repository
{
    public interface IAuthorProfileRepository
    {
        Task<AuthorProfile> GetUserProfile(string name, string email);
        Task<int> Create(AuthorProfile userProfile, DbConnection dbConnection, DbTransaction dbTransaction);
    }
}
