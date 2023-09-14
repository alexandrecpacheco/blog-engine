using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Intefaces;
using BlogEngine.Domain.Intefaces.Data.Repository;
using Dapper;
using System.Data.Common;

namespace BlogEngine.Infrastructure.Data
{
    public class SubmitRepository : ISubmitRepository
    {
        private readonly IDatabase _database;

        public SubmitRepository(IDatabase database)
        {
            _database = database;
        }

        public async Task<int> GetSubmitPostByIdAsync(int postId)
        {
            await using var conn = await _database.CreateAndOpenConnection();
            const string query = @"
                SELECT post_id
                    FROM posts p
                    INNER JOIN submits s ON
                        p.post_id = s.post_id
                WHERE s.post_id = @PostId
                    AND s.publish_type = 'R'
            ";

            var parameters = new { postId };
            return await conn.QueryFirstOrDefaultAsync<int>(query, parameters);
        }

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
