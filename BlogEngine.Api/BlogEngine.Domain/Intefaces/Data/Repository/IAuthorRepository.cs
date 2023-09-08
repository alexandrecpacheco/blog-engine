using BlogEngine.Domain.Entities;
using System.Data.Common;

namespace BlogEngine.Domain.Intefaces.Data.Repository
{
    public interface IAuthorRepository
    {
        Task<Author> GetByEmailAndPasswordAsync(Author authorEntity);
        Task<Author> GetByEmailAsync(string email);
        Task<int> CreateAsync(Author author, DbConnection dbConnection, DbTransaction dbTransaction);
    }
}
