namespace BlogEngine.Domain.Entities
{
    public class ProfileEntity : BaseEntity
    {
        public ProfileEntity()
        {
            AuthorProfileCollection = new HashSet<AuthorProfileEntity>();
        }

        public int ProfileId { get; set; }
        public string Description { get; set; }
        public ICollection<AuthorProfileEntity> AuthorProfileCollection { get; set; }
    }
}
