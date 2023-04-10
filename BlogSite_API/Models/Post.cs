using System.ComponentModel.DataAnnotations;

namespace BlogSite_API.Models
{
    public class Post 
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public string? PostDate { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
