namespace BlogEngine.Domain.Entities
{
    public class Posts : BaseEntity
    {
        public Posts() 
        {
            Comments = new HashSet<Comments>();
        }

        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int CommentId { get; set; }
        public string Title { get; set; }
        public DateTime PublishDateTime { get; set; }
        public bool Approved { get; set; }
        public AuthorProfile AuthorProfiles { get; set; }
        public ICollection<Comments> Comments { get; set; }
    }
}
