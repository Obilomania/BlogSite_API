using BlogSite_API.Data;
using BlogSite_API.Models;
using BlogSite_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BlogSite_API.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _db;

        public PostRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreatePostAsync(Post post)
        {
           await _db.Posts.AddAsync(post);
            _db.SaveChanges();
        }

        public async Task DeletePostAsync(Post post)
        {
            _db.Posts.Remove(post);
            await SaveAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _db.Posts.ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _db.Posts.Include(c => c.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task PostExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            _db.Posts.Update(post);
            await SaveAsync();
        }
    }
}
