namespace BlogEngine.Domain.Entities
{
    public class SubmitEntity
    {
        public int SubmitId { get; set; }
        public int PostId { get; set; }
        public char PublishType { get; set; }
        public string? Comment { get; set; }
    }
}
