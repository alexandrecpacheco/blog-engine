using BlogEngine.Domain.Dto.Request;

namespace BlogEngine.Domain.Intefaces.Data.Service
{
    public interface ISubmitService
    {
        Task UpdateAsync(SubmitRequest request);
    }
}
