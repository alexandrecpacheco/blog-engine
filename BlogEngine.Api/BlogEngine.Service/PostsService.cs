using AutoMapper;
using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain.Dto.Response;
using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Enums;
using BlogEngine.Domain.Intefaces;
using BlogEngine.Domain.Intefaces.Data.Repository;
using BlogEngine.Domain.Intefaces.Data.Service;

namespace BlogEngine.Service
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _postsRepository;
        private readonly ICommentsRepository _commentsRepository;
        private readonly IDatabase _database;
        private readonly IMapper _mapper;

        public PostsService(IPostsRepository postsRepository, IDatabase database, IMapper mapper, ICommentsRepository commentsRepository)
        {
            _postsRepository = postsRepository;
            _database = database;
            _mapper = mapper;
            _commentsRepository = commentsRepository;
        }

        public async Task CreateAsync(PostRequest request)
        {
            await _database.ExecuteInTransaction(async (connection, transaction) =>
            {
                var post = new PostsEntity()
                {
                    AuthorProfileId = request.AuthorProfileId,
                    PublishDate = null,
                    PublishType = (char)PublishType.PendingApproval,
                    ReadOnlyByAuthor = false,
                    Title = request.Title,
                };

                var postId = await _postsRepository.Create(post, connection, transaction);

                var comment = new CommentsEntity()
                {
                    PostId = postId,
                    Comment = request.Comment.Comment,
                    ReadbleByAuthorProfileId = request.Comment.ReadbleByAuthorProfileId
                };

                await _commentsRepository.Create(comment, connection, transaction);
            });
        }

        public async Task<IEnumerable<PostsResponse>> GetPostsAsync()
        {
            var result = await _postsRepository.GetPosts();
            if (result == null) return default!;

            var mapped = _mapper.Map<IEnumerable<PostsResponse>>(result);
            return mapped;
        }
    }
}
