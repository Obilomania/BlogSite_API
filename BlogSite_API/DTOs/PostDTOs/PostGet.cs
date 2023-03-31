using BlogSite_API.Models;

namespace BlogSite_API.DTOs.PostDTOs
{
    public class PostGet
    {
        public int PostId { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public DateTime CreatedOn { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
