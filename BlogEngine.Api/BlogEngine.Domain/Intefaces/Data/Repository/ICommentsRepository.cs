using BlogEngine.Domain.Entities;
using System.Data.Common;

namespace BlogEngine.Domain.Intefaces.Data.Repository
{
    public interface ICommentsRepository
    {
        Task<int> Create(CommentsEntity comment, DbConnection dbConnection, DbTransaction dbTransaction);
    }
}
