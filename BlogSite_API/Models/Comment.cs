namespace BlogSite_API.Models
{
    public class Comment : BaseEntity
    {
        public int CommentId { get; set; }
        public string UserEmail { get; set; } = default!;
        public string CommentContent { get; set; } = default!;
        public DateTime CommentedOn { get; set; }
        public Post Post { get; set; } = default!;
    }
}
