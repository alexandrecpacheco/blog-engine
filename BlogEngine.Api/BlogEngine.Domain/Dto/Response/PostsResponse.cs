﻿namespace BlogEngine.Domain.Dto.Response
{
    public class PostsResponse
    {
        public int AuthorProfileId { get; set; }
        public string Title { get; set; }
        public char PublishType { get; set; }
        public bool ReadOnlyByAuthor { get; set; }
        public DateTime PublishDate { get; set; }
        public ICollection<CommentsResponse> Comments { get; set; }
    }
}
