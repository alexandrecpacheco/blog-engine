using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Intefaces;
using BlogEngine.Domain.Intefaces.Data.Repository;
using Dapper;
using System.Data.Common;

namespace BlogEngine.Infrastructure.Data
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IDatabase _database;
        public AuthorRepository(IDatabase database) 
        { 
            _database = database;
        }

        public Task<int> CreateAsync(AuthorEntity author, DbConnection dbConnection, DbTransaction dbTransaction)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthorEntity> GetByEmailAndPasswordAsync(AuthorEntity authorEntity)
        {
            await using var conn = await _database.CreateAndOpenConnection();

            const string query = @"
                    SELECT a.author_id, a.author_key, a.name, a.email, a.is_active, a.created_at, a.updated_at,
                           ap.author_profile_id, 
                           p.profile_id, p.description
                    FROM [author] a
                    INNER JOIN author_profile ap
                        ON a.author_id = ap.author_id
                    INNER JOIN profile p 
                        ON ap.profile_id = p.profile_id
                    WHERE email = @Email 
                    AND password = @Password";

            var parameters = new { authorEntity.Email, authorEntity.Password };
            var authorDictionary = new Dictionary<int, AuthorEntity>();
            var result = await conn.QueryAsync<AuthorEntity, AuthorProfileEntity, ProfileEntity, AuthorEntity>(query,
                (author, authorProfile, profile) =>
                {
                    if (authorDictionary.TryGetValue(author.AuthorId, out var authorResponse) == false)
                    {
                        authorResponse = author;
                        authorDictionary.Add(authorResponse.AuthorId, authorResponse);
                    }

                    if (authorProfile != null)
                    {
                        authorResponse.AuthorProfile.AuthorProfileId = authorProfile.AuthorProfileId;
                        authorResponse.AuthorProfile.Profile.ProfileId = profile.ProfileId;
                        authorResponse.AuthorProfile.Profile.Description = profile.Description;

                    }
                    return authorResponse;
                }, parameters, splitOn: "author_profile_id, profile_id");

            return result.FirstOrDefault();
        }

        public async Task<AuthorEntity> GetByEmailAsync(string email)
        {
            await using var conn = await _database.CreateAndOpenConnection();
            const string query = @"
                    SELECT a.author_id, a.name, a.email
                    FROM [author] a
                    WHERE a.email = @Email
            ";

            var parameters = new { email };
            return await conn.QueryFirstOrDefaultAsync<AuthorEntity>(query, parameters);
        }
    }
}
