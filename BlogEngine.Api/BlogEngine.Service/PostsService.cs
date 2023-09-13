using AutoMapper;
using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain.Dto.Response;
using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Intefaces;
using BlogEngine.Domain.Intefaces.Data.Repository;
using BlogEngine.Domain.Intefaces.Data.Service;

namespace BlogEngine.Service
{
    public class PostsService : IPostsService
    {
        private readonly IPostsRepository _postsRepository;
        private readonly IDatabase _database;
        private readonly IMapper _mapper;

        public PostsService(IPostsRepository postsRepository, IDatabase database, IMapper mapper)
        {
            _postsRepository = postsRepository;
            _database = database;
            _mapper = mapper;
        }

        public async Task CreateAsync(PostRequest request)
        {
            await _database.ExecuteInTransaction(async (connection, transaction) =>
            {
                var post = new PostsEntity()
                {
                    AuthorProfileId = request.AuthorProfileId,
                    Title = request.Title,
                    Description = request.Description,
                    PublishDate = null,
                    ReadOnlyByAuthor = false,
                };

                await _postsRepository.Create(post, connection, transaction);
            });
        }

        public async Task<IEnumerable<PostsResponse>> GetPostsAsync()
        {
            var result = await _postsRepository.GetPosts();
            if (result == null) return default!;

            var mapped = _mapper.Map<IEnumerable<PostsResponse>>(result);
            return mapped;
        }

        public async Task<bool> GetPublishedPostByIdAsync(int postId)
        {
            var result = await _postsRepository.GetPublishedPostByIdAsync(postId);

            return result;
        }
    }
}
