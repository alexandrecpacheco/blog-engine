using System.Diagnostics.CodeAnalysis;

namespace BlogEngine.Domain.Dto.Request
{
    [ExcludeFromCodeCoverage]
    public class CommentRequest
    {
        public string Comment { get; set; }
        public int ReadbleByAuthorProfileId { get; set; }
    }
}