using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain.Entities;
using BlogEngine.Domain.Enums;
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
                bool @readonly = false;
                if (request.PublicType == (char)PublishType.Rejected)
                {
                    @readonly = true;
                }

                var submit = new SubmitEntity()
                {
                    PostId = postId,
                    PublishType = request.PublicType.ToString(),
                    Comment = request.Comment,
                    PublishDate = @readonly ? null : DateTime.UtcNow
                };

                await _submitRepository.Update(submit, @readonly, connection, transaction);
            });
        }
    }
}
