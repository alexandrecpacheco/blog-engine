using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain.Dto.Response;

namespace BlogEngine.Domain.Intefaces.Data.Service
{
    public interface IPostsService
    {
        Task CreateAsync(PostRequest request);
        Task UpdateAsync(PostUpdateRequest request);
        Task<IEnumerable<PostsResponse>> GetPostsAsync();
        Task<bool> GetPublishedPostByIdAsync(int postId);

    }
}
