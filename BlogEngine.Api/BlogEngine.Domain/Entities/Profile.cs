namespace BlogEngine.Domain.Entities
{
    public class Profile : BaseEntity
    {
        public Profile()
        {
            AuthorProfileCollection = new HashSet<AuthorProfile>();
        }

        public int ProfileId { get; set; }
        public string Description { get; set; }
        public ICollection<AuthorProfile> AuthorProfileCollection { get; set; }
    }
}
