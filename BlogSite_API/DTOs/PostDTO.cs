using BlogSite_API.Models;

namespace BlogSite_API.DTOs
{
    public class PostDTO
    {
        public int PostId { get; set; }


        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
