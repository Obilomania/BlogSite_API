using System.ComponentModel.DataAnnotations;

namespace BlogSite_API.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }


        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
