using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Intefaces.Data.Repository;
using Dapper;
using System.Data.Common;

namespace BlogEngine.Infrastructure.Data
{
    public class SubmitRepository : ISubmitRepository
    {
        public async Task Update(SubmitEntity submit, bool @readonly, DbConnection dbConnection, DbTransaction dbTransaction)
        {
            const string query = @"
            UPDATE submits
                    SET publish_type = @PublishType,
                    publish_date = @PublishDate,
                    comment = @Comment
            WHERE post_id = @PostId

            UPDATE posts
                    SET readonly_by_author = @Readonly
            WHERE post_id = @PostId
            ";
            
            var parameters = new { submit.PublishType, submit.PublishDate, submit.Comment, submit.PostId, @readonly };
            await dbConnection.ExecuteAsync(query, parameters, dbTransaction);
        }
    }
}
