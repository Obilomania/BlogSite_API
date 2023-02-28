using BlogSite_API.Models;

namespace BlogSite_API.DTOs.PostDTOs
{
    public class PostCreate
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public DateTime CreatedOn { get; set; }
        public List<int> Comments { get; set; } = default!;
    }
}
