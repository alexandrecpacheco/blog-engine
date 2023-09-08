using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Intefaces;
using BlogEngine.Domain.Intefaces.Data.Repository;
using Dapper;
using System.Data.Common;

namespace BlogEngine.Infrastructure.Data
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly IDatabase _database;
        public CommentsRepository(IDatabase database)
        {
            _database = database;
        }

        public async Task<int> Create(Comments comments, DbConnection dbConnection, DbTransaction dbTransaction)
        {
            //TODO: Need to be created
            const string query = @"
                    INSERT INTO posts (user_profile_id, [description], start_at, end_at)
                    VALUES (@UserProfileId, @Description, @StartAt, @EndAt)
            
                    SELECT @@IDENTITY;
            ";

            return await dbConnection.QuerySingleAsync<int>(query, comments, dbTransaction);
        }
    }
}
