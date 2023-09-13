namespace BlogEngine.Domain.Dto.Response
{
    public class PostsResponse
    {
        public int AuthorId { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public bool Approved { get; set; }
        public bool ReadOnlyByAuthor { get; set; }
    }
}
