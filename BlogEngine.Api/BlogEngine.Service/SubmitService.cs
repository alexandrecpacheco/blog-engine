using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Intefaces;
using BlogEngine.Domain.Intefaces.Data.Repository;
using BlogEngine.Domain.Intefaces.Data.Service;

namespace BlogEngine.Service
{
    public class SubmitService : ISubmitService
    {
        private readonly IPostsRepository _postsRepository;
        private readonly ISubmitRepository _submitRepository;
        private readonly IDatabase _database;

        public SubmitService(ISubmitRepository submitRepository, IDatabase database, IPostsRepository postsRepository)
        {
            _submitRepository = submitRepository;
            _database = database;
            _postsRepository = postsRepository;
        }

        public async Task UpdateAsync(SubmitRequest request)
        {
            var postId = await _postsRepository.GetPostByIdAsync(request.PostId);

            if (postId <= 0)
                throw new InvalidOperationException("The Post mentioned does not exists");

            await _database.ExecuteInTransaction(async (connection, transaction) =>
            {
                var submit = new SubmitEntity()
                {
                    PostId = postId,
                    PublishType = request.PublicType,
                    Comment = request.Comment
                };

                await _submitRepository.Update(submit, connection, transaction);
            });
        }
    }
}
