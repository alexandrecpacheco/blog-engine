using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Intefaces;
using BlogEngine.Domain.Intefaces.Data.Repository;
using Dapper;
using System.Data.Common;

namespace BlogEngine.Infrastructure.Data
{
    public class PostsRepository : IPostsRepository
    {
        private readonly IDatabase _database;
        public PostsRepository(IDatabase database)
        {
            _database = database;
        }

        public async Task<int> Create(PostsEntity posts, DbConnection dbConnection, DbTransaction dbTransaction)
        {
            //TODO: Need to be created
            const string query = @"
                    INSERT INTO posts (user_profile_id, [description], start_at, end_at)
                    VALUES (@UserProfileId, @Description, @StartAt, @EndAt)
            
                    SELECT @@IDENTITY;
            ";

            return await dbConnection.QuerySingleAsync<int>(query, posts, dbTransaction);
        }

        public async Task<IEnumerable<PostsEntity>> GetPosts()
        {
            await using var conn = await _database.CreateAndOpenConnection();
            const string query = @"
                SELECT * FROM posts
            ";  
            return await conn.QueryAsync<PostsEntity>(query);
        }
    }
}
