﻿using System.Diagnostics.CodeAnalysis;

namespace BlogEngine.Domain.Dto.Request
{
    [ExcludeFromCodeCoverage]
    public class PostRequest
    {
        public int AuthorProfileId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public char PublishType { get; set; }
        public bool ReadOnlyByAuthor { get; set; }
    }
}
