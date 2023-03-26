using BlogSite_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSite_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LocalUser> Localusers { get; set; }
    }
}
