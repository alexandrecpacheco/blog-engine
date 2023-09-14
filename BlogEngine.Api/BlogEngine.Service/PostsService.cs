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
        private readonly ISubmitService _submitService;
        private readonly IDatabase _database;
        private readonly IMapper _mapper;

        public PostsService(IPostsRepository postsRepository, IDatabase database, IMapper mapper, ISubmitService submitService)
        {
            _postsRepository = postsRepository;
            _database = database;
            _mapper = mapper;
            _submitService = submitService;
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
                    ReadOnlyByAuthor = false,
                };

                await _postsRepository.Create(post, connection, transaction);
            });
        }

        public async Task UpdateAsync(PostUpdateRequest request)
        {
            var postId = await _postsRepository.GetPostByIdAsync(request.PostId);

            if (postId <= 0)
                throw new InvalidOperationException("The Post mentioned does not exists");

            var submitId = await _submitService.GetSubmitPostByIdAsync(request.PostId);
            if (submitId <= 0)
                throw new InvalidOperationException("The Post cannot be modified");

            await _database.ExecuteInTransaction(async (connection, transaction) =>
            {
                var post = new PostsEntity()
                {
                    PostId = postId,
                    Title = request.Title,
                    Description = request.Description,
                    ReadOnlyByAuthor = request.ReadOnlyByAuthor,
                };

                await _postsRepository.Update(post, connection, transaction);
            });
        }

        public async Task<IEnumerable<PostsResponse>> GetPublishedPostsAsync()
        {
            var result = await _postsRepository.GetPublishedPosts();
            if (result == null) return default!;

            var mapped = _mapper.Map<IEnumerable<PostsResponse>>(result);
            return mapped;
        }

        public async Task<bool> GetPublishedPostByIdAsync(int postId)
        {
            var result = await _postsRepository.GetPublishedPostByIdAsync(postId);

            return result;
        }
        
        public async Task<IEnumerable<PostsResponse>> GetPendingPostsAsync()
        {
            var result = await _postsRepository.GetPendingPostsAsync();
            if (result == null) return default!;

            var mapped = _mapper.Map<IEnumerable<PostsResponse>>(result);

            return mapped;
        }
    }
}
