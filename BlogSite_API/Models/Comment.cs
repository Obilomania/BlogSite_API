namespace BlogSite_API.Models
{
    public class Comment 
    {
        public int Id { get; set; }
        public string CommentContent { get; set; } = default!;
        public DateTime CommentedOn { get; set; }
        public virtual Post? Post { get; set; } = default!;
        public int PostId { get; set; }
    }
}
