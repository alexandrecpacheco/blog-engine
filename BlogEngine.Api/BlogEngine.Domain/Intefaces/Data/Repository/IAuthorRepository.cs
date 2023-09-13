using BlogEngine.Domain.Entities;
using System.Data.Common;

namespace BlogEngine.Domain.Intefaces.Data.Repository
{
    public interface IAuthorRepository
    {
        Task<AuthorEntity> GetByEmailAndPasswordAsync(AuthorEntity authorEntity);
        Task<AuthorEntity> GetByEmailAsync(string email);
        Task<int> CreateAsync(AuthorEntity author, DbConnection dbConnection, DbTransaction dbTransaction);
    }
}
