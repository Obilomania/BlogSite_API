namespace BlogSite_API.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string UserEmail { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentedOn { get; set; }


        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
