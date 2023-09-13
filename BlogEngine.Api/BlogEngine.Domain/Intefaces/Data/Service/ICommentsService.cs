using BlogEngine.Domain.Dto.Request;

namespace BlogEngine.Domain.Intefaces.Data.Service
{
    public interface ICommentsService
    {
        Task CreateAsync(CommentRequest request);
    }
}
