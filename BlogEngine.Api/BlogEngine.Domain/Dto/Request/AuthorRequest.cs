namespace BlogEngine.Domain.Dto.Request
{
    public class AuthorRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public AuthorProfileRequest AuthorProfile { get; set; }
    }
}
