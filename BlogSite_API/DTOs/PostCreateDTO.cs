namespace BlogSite_API.DTOs
{
    public class PostCreateDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
