namespace BlogSite_API.DTOs.PostDTOs
{
    public class PostUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; } 
        public IFormFile? ImageFile { get; set; }
    }
}
