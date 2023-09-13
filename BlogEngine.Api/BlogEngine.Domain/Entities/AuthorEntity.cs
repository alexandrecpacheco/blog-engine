using System.Diagnostics.CodeAnalysis;

namespace BlogEngine.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class AuthorEntity : BaseEntity

    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public AuthorProfileEntity AuthorProfile { get; set; } = new AuthorProfileEntity();
    }
}
