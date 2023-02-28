namespace BlogSite_API.DTOs.PostDTOs
{
    public class PostGet
    {
        public string Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public DateTime CreatedOn { get; set; }
    }
}
