using BlogEngine.Domain.Entities;
using System.Data.Common;

namespace BlogEngine.Domain.Intefaces.Data.Repository
{
    public interface ISubmitRepository
    {
        Task Update(SubmitEntity submit, DbConnection dbConnection, DbTransaction dbTransaction);
    }
}
