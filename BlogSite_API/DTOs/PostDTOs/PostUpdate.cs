namespace BlogSite_API.DTOs.PostDTOs
{
    public class PostUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public DateTime CreatedOn { get; set; }
    }
}
