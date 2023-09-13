namespace BlogEngine.Domain.Dto.Response
{
    public class CommentsResponse
    {
        public int PostId { get; set; }
        public string? Comment { get; set; }
        public int ReadbleBy { get; set; }
    }
}
