using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Intefaces.Data.Repository;
using Dapper;
using System.Data.Common;

namespace BlogEngine.Infrastructure.Data
{
    public class CommentsRepository : ICommentsRepository
    {
        public async Task<int> Create(CommentsEntity comment, DbConnection dbConnection, DbTransaction dbTransaction)
        {
            const string query = @"
                INSERT INTO comments (post_id, comment, readble_by_author_profile_id)
                VALUES (@PostId, @Comment, @ReadbleByAuthorProfileId)
                
                SELECT @@IDENTITY;
            ";

            return await dbConnection.QuerySingleAsync<int>(query, comment, dbTransaction);
        }
    }
}
