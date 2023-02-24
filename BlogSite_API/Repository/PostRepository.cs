using BlogSite_API.Data;
using BlogSite_API.Models;
using BlogSite_API.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BlogSite_API.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Post> GetAllPosts()
        {
            return _context.Posts.Include(p => p.Comments).ToList();
        }

        public Post GetById(int id)
        {
            return _context.Posts
                .Where(u =>u.PostId == id)
                .Include(c => c.Comments)
                .FirstOrDefault(p => p.PostId == id);
        }
    }
}

