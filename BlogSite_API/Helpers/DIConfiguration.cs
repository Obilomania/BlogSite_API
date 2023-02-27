using BlogSite_API.Repository;
using BlogSite_API.Repository.IRepository;

namespace BlogSite_API.Helpers
{
    public class DIConfiguration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
        }
    }
}
