namespace BlogSite_API.DTOs.CommentDTOs
{
    public class CommentCreate
    {
        public string CommentContent { get; set; } = default!;
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string? Commenter { get; set; }
    }
}
