namespace BlogEngine.Domain.Entities
{
    public class AuthorProfile : BaseEntity
    {
        public int AuthorProfileId { get; set; }
        public int ProfileId { get; set; }
        public int AuthorId { get; set; }
        public Profile Profile { get; set; } = new Profile();
    }
}
