using System.ComponentModel.DataAnnotations;

namespace BlogSite_API.DTOs.PostDTOs
{
    public class PostCreate
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public IFormFile ImageFile { get; set; } = default!;
    }
}
