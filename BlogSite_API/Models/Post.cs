namespace BlogSite_API.Models
{
    public class Post : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public DateTime CreatedOn { get; set; }
        public List<Comment> Comments { get; set; } = default!;
    }
}
