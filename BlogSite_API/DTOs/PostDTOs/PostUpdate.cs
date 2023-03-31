namespace BlogSite_API.DTOs.PostDTOs
{
    public class PostUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public IFormFile ImageFile { get; set; } = default!;
        public DateTime? UpdatedTime { get; set; } = DateTime.UtcNow;
    }
}
