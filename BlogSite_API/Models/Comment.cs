namespace BlogSite_API.Models
{
    public class Comment 
    {
        public int Id { get; set; }
        public string CommentContent { get; set; } = default!;
        public string? CommentedOn { get; set; }
        public virtual Post? Post { get; set; } = default!;
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string? Commenter { get; set; }
    }
}
