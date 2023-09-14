using BlogEngine.Domain.Dto.Request;
using BlogEngine.Domain.Dto.Response;

namespace BlogEngine.Domain.Intefaces.Data.Service
{
    public interface IPostsService
    {
        Task CreateAsync(PostRequest request);
        Task UpdateAsync(PostUpdateRequest request);
        Task<IEnumerable<PostsResponse>> GetPublishedPostsAsync();
        Task<bool> GetPublishedPostByIdAsync(int postId);
        Task<IEnumerable<PostsResponse>> GetPendingPostsAsync();
    }
}
