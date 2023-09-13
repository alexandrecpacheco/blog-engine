namespace BlogEngine.Domain.Dto.Request
{
    public class SubmitRequest
    {
        public int PostId { get; set; }
        public char PublicType { get; set; }
        public string? Comment { get; set; }
    }
}
