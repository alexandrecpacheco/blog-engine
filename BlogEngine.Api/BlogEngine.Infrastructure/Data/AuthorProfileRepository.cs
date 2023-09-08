using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Intefaces;
using BlogEngine.Domain.Intefaces.Data.Repository;
using Dapper;
using System.Data.Common;

namespace BlogEngine.Infrastructure.Data
{
    public class AuthorProfileRepository : IAuthorProfileRepository
    {
        private readonly IDatabase _database;
        public AuthorProfileRepository(IDatabase database)
        {
            _database = database;
        }

        public async Task<int> Create(AuthorProfile userProfile, DbConnection dbConnection, DbTransaction dbTransaction)
        {
            const string query = @"
                    INSERT INTO user_profile (user_id, profile_id)
                    VALUES (@UserId, @ProfileId);
            
                    SELECT @@IDENTITY;
            ";

            return await dbConnection.QuerySingleAsync<int>(query, userProfile, dbTransaction);
        }

        public async Task<AuthorProfile> GetUserProfile(string name, string email)
        {
            await using var conn = await _database.CreateAndOpenConnection();

            string query = @"
                    SELECT up.user_profile_id, up.user_id,
                           p.profile_id, p.description
                    FROM [user] u
                    INNER JOIN user_profile up
                        ON u.user_id = up.user_id
                    INNER JOIN profile p 
                        ON up.profile_id = p.profile_id
                    WHERE name = @Name
                    ";

            query += !string.IsNullOrWhiteSpace(email) ? " AND email = @Email" : string.Empty;

            var parameters = new { email, name };
            var userDictionary = new Dictionary<int, AuthorProfile>();
            var result = await conn.QueryAsync<AuthorProfile, Profile, AuthorProfile>(query,
                (userProfile, profile) =>
                {
                    if (userDictionary.TryGetValue(userProfile.AuthorId, out var userResponse) == false)
                    {
                        userResponse = userProfile;
                        userDictionary.Add(userResponse.AuthorId, userResponse);
                    }

                    if (userProfile != null)
                    {
                        userResponse.AuthorProfileId = userProfile.AuthorProfileId;
                        userResponse.Profile.ProfileId = profile.ProfileId;
                        userResponse.Profile.Description = profile.Description;

                    }
                    return userResponse;
                }, parameters, splitOn: "profile_id");

            return result.FirstOrDefault()!;
        }
    }
}
