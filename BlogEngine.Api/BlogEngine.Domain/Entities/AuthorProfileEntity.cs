using System.Diagnostics.CodeAnalysis;
namespace BlogEngine.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class AuthorProfileEntity : BaseEntity
    {
        public int AuthorProfileId { get; set; }
        public int ProfileId { get; set; }
        public int AuthorId { get; set; }
        public ProfileEntity Profile { get; set; } = new ProfileEntity();
    }
}
