using AutoMapper;
using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Intefaces;
using BlogEngine.Domain.Intefaces.Data.Repository;
using BlogEngine.Domain.Intefaces.Data.Service;

namespace BlogEngine.Service
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IPostsService _postsService;
        private readonly IDatabase _database;
        private readonly IMapper _mapper;

        public CommentsService(IDatabase database, IMapper mapper, ICommentsRepository commentsRepository, IPostsService postsService)
        {
            _database = database;
            _mapper = mapper;
            _commentsRepository = commentsRepository;
            _postsService = postsService;
        }

        public async Task CreateAsync(CommentRequest request)
        {
            var publishedPost = await _postsService.GetPublishedPostByIdAsync(request.PostId);
            if (publishedPost is false)
                throw new Exception("The Post mentioned is not published. Please, try again with a valid Post");

            await _database.ExecuteInTransaction(async (connection, transaction) =>
            {
                var comment = new CommentsEntity()
                {
                    PostId = request.PostId,
                    Comment = request.Comment,
                    ReadbleByAuthorProfileId = request.ReadbleByAuthorProfileId
                };

                await _commentsRepository.Create(comment, connection, transaction);
            });
        }
    }
}
