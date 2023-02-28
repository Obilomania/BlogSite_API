namespace BlogSite_API.Models
{
    public class Comment : BaseEntity
    {
        public string CommentContent { get; set; } = default!;
        public DateTime CommentedOn { get; set; }
        public Post Post { get; set; } = default!;
    }
}
