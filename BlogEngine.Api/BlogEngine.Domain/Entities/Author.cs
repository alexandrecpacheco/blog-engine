namespace BlogEngine.Domain.Entities
{
    public class Author : BaseEntity
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public AuthorProfile AuthorProfile { get; set; } = new AuthorProfile();
    }
}
