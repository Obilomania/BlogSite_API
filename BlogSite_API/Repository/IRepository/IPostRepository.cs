using BlogSite_API.Models;

namespace BlogSite_API.Repository.IRepository
{
    public interface IPostRepository
    {
        ICollection<Post> GetAllPosts();
        Post GetById(int id);
    }
}
