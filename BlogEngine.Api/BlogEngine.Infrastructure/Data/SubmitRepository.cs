using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Intefaces.Data.Repository;
using Dapper;
using System.Data.Common;

namespace BlogEngine.Infrastructure.Data
{
    public class SubmitRepository : ISubmitRepository
    {
        public async Task Update(SubmitEntity submit, DbConnection dbConnection, DbTransaction dbTransaction)
        {
            const string query = @"
            UPDATE submits
                    SET publish_type = @PublishType,
                    comment = @Comment
            WHERE post_id = @PostId
            ";

            await dbConnection.ExecuteAsync(query, submit, dbTransaction);
        }
    }
}
