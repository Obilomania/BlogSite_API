namespace BlogSite_API.Models
{
    public class Post : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public DateTime PostedOn { get; set; }
        public List<Comment>? Comments { get; set; } = default!;

        //public string? ApplicationUserId { get; set; }
        //public ApplicationUser? Poster { get; set; } = default!;
    }
}
