namespace BlogEngine.Domain.Dto.Response
{
    public class PostsResponse
    {
        public int AuthorProfileId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool ReadOnlyByAuthor { get; set; }
        public IEnumerable<CommentsResponse> Comments { get; set; }
        public SubmitResponse Submit { get; set; }
    }

    public class SubmitResponse
    {
        public int SubmitId { get; set; }
        public DateTime PublishDate { get; set; }
        public string PublishType { get; set; }
    }
}
