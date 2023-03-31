namespace BlogSite_API.DTOs.CommentDTOs
{
    public class CommentCreate
    {
        public string CommentContent { get; set; } = default!;
        public DateTime CommentedOn { get; set; } = DateTime.Now;
        public int PostId { get; set; }
    }
}
