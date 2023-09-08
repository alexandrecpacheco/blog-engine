namespace BlogEngine.Domain.Entities
{
    public class Comments : BaseEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string? Comment { get; set; }
        public string? ReadbleBy { get; set; }
        public Posts Posts { get; set; }
    }
}
