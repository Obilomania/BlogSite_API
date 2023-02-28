using BlogSite_API.DTOs.PostDTOs;

namespace BlogSite_API.Repository.IRepository
{
    public interface IPostRepository
    {
        Task<List<PostGet>> GetAllPostAsync();
        Task<PostGet> GetPostAsync(int id);
        Task<int> CreatePostAsync(PostCreate postCreate);
        Task UpdatePostAsync(PostUpdate postUpdate);
        Task DeletePostAsync(PostDelete postDelete);
    }
}
