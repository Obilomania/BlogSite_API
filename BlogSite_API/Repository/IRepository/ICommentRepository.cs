using BlogSite_API.DTOs.CommentDTOs;

namespace BlogSite_API.Repository.IRepository
{
    public interface ICommentRepository
    {
        Task<int> CreateCommentAsync(CommentCreate commentCreate);
        Task DeleteCommentasync(CommentDelete commentDelete);
    }
}
