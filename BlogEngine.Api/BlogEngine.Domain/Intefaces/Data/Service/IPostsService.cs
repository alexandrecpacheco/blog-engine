using BlogEngine.Domain.Dto.Response;

namespace BlogEngine.Domain.Intefaces.Data.Service
{
    public interface IPostsService
    {
        Task<IEnumerable<PostsResponse>> GetPostsAsync();
    }
}
