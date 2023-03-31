namespace BlogSite_API.Models
{
    public class Post 
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public DateTime? PostDate { get; set; } = DateTime.UtcNow;
        public ICollection<Comment>? Comments { get; set; }
    }
}
