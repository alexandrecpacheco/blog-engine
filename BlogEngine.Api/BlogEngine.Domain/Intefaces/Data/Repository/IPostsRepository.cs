using BlogEngine.Domain.Entities;
using System.Data.Common;

namespace BlogEngine.Domain.Intefaces.Data.Repository
{
    public interface IPostsRepository
    {
        Task<int> Create(Posts posts, DbConnection dbConnection, DbTransaction dbTransaction);
    }
}
