using System.Diagnostics.CodeAnalysis;

namespace BlogEngine.Domain.Dto.Request
{
    [ExcludeFromCodeCoverage]
    public class PostUpdateRequest
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool ReadOnlyByAuthor { get; set; }
    }
}
