using System.ComponentModel.DataAnnotations;

namespace BlogSite_API.DTOs
{
    public class PostUpdateDTO
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
