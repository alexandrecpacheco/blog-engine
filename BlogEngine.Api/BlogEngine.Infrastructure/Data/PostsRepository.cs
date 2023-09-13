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
            const string query = @"
                    INSERT INTO posts(author_profile_id, title, [description], publish_type, publish_date, readonly_by_author)
                    values (@AuthorProfileId, @Title, @Description, @PublishType, @PublishDate, @ReadonlyByAuthor)
            
                    SELECT @@IDENTITY;
            ";

            return await dbConnection.QuerySingleAsync<int>(query, posts, dbTransaction);
        }

        public async Task Update(PostsEntity posts, DbConnection dbConnection, DbTransaction dbTransaction)
        {
            const string query = @"
                UPDATE posts
                        SET title = @Title,
                        description = @Description
                        readonly_by_author = @ReadOnlyByAuthor
                WHERE post_id = @PostId
            ";

            await dbConnection.ExecuteAsync(query, posts, dbTransaction);
        }

        public async Task<IEnumerable<PostsEntity>> GetPosts()
        {
            await using var conn = await _database.CreateAndOpenConnection();
            const string query = @"
                SELECT p.post_id, p.author_profile_id, p.publish_type, p.readonly_by_author, p.title, p.publish_date,
                    c.comment_id, c.post_id, c.readble_by, c.comment_id, c.comment
                    FROM posts p
                    INNER JOIN comments c ON
	                    c.post_id = p.post_id
            ";

            var postsDictionary = new Dictionary<int, PostsEntity>();
            var commentsDictionary = new Dictionary<int, CommentsEntity>();
            var result = await conn.QueryAsync<PostsEntity, CommentsEntity, PostsEntity>(query,
                (posts, comments) =>
                {
                    if (posts != null)
                    {
                        if (postsDictionary.TryGetValue(posts.PostId, out var postResponse) == false)
                        {
                            postResponse = posts;
                            postsDictionary.Add(postResponse.PostId, postResponse);
                        }

                        if (comments != null)
                        {
                            if (commentsDictionary.TryGetValue(comments.CommentId, out var commentResponse) == false)
                            {
                                commentResponse = comments;
                                postResponse.Comments.Add(commentResponse);
                            }
                        }

                        return postResponse;
                    }

                    return new PostsEntity();

                }, splitOn: "post_id, comment_id");

            return result.ToList();
        }

        public async Task<bool> GetPublishedPostByIdAsync(int postId)
        {
            await using var conn = await _database.CreateAndOpenConnection();
            const string query = @"
                SELECT post_id
                    FROM posts
                WHERE post_id = @PostId
                AND publish_date IS NOT NULL
            ";
            var parameters = new { postId };

            return await conn.QueryFirstOrDefaultAsync<bool>(query, parameters);
        }

        public async Task<int> GetPostByIdAsync(int postId)
        {
            await using var conn = await _database.CreateAndOpenConnection();
            const string query = @"
                SELECT post_id
                    FROM posts
                WHERE post_id = @PostId
            ";
            var parameters = new { postId };

            return await conn.QueryFirstOrDefaultAsync<int>(query, parameters);
        }
    }
}
