namespace BlogEngine.Domain.Entities
{
    public class SubmitEntity
    {
        public int SubmitId { get; set; }
        public int PostId { get; set; }
        public string PublishType { get; set; }
        public DateTime? PublishDate { get; set; }
        public string? Comment { get; set; }
    }
}
