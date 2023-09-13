using System.Diagnostics.CodeAnalysis;

namespace BlogEngine.Domain.Dto.Request
{
    [ExcludeFromCodeCoverage]
    public class CommentRequest
    {
        public int PostId { get; set; }
        public string Comment { get; set; }
        public int ReadbleByAuthorProfileId { get; set; }
    }
}