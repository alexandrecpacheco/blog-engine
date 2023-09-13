using System.Diagnostics.CodeAnalysis;

namespace BlogEngine.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class PostsEntity : BaseEntity
    {
        public PostsEntity()
        {
            Comments = new HashSet<CommentsEntity>();
        }

        public int PostId { get; set; }
        public int AuthorProfileId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool ReadOnlyByAuthor { get; set; }
        public DateTime? PublishDate { get; set; }
        public AuthorProfileEntity? AuthorProfile { get; set; }
        public ICollection<CommentsEntity> Comments { get; set; }
    }
}