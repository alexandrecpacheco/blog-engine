namespace BlogEngine.Domain.Entities
{
    public class CommentsEntity : BaseEntity
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string Comment { get; set; }
        public int? ReadbleByAuthorProfileId { get; set; }
    }
}
