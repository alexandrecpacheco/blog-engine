namespace BlogEngine.Domain.Dto.Response
{
    public class AuthorAuthenticatedResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
