using System.Diagnostics.CodeAnalysis;

namespace BlogEngine.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class PostsEntity : BaseEntity
    {
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public bool Approved { get; set; }
        public bool ReadOnlyByAuthor { get; set; }
        public AuthorProfileEntity AuthorProfile { get; set; }
    }
}
