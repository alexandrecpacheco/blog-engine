using BlogEngine.Domain.Dto.Request;

namespace BlogEngine.Domain.Intefaces.Data.Service
{
    public interface ISubmitService
    {

        Task<int> GetSubmitPostByIdAsync(int postId);
        Task UpdateAsync(SubmitRequest request);
    }
}
