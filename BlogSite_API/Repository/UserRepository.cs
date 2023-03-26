using BlogSite_API.Data;
using BlogSite_API.DTOs.UserData;
using BlogSite_API.Models;
using BlogSite_API.Repository.IRepository;

namespace BlogSite_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.Localusers.FirstOrDefault(x=> x.UserName == username);
            if (user == null) { return true; }
            return false;
        }

        public async Task<LoginRequest> Login(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<LocalUser> Register(RegisterationRequest registerationRequest)
        {
            LocalUser user = new LocalUser()
            {
                UserName = registerationRequest.UserName,
                Password = registerationRequest.Password,
                Name = registerationRequest.Name,
                Role = registerationRequest.Role,
            };
            _db.Localusers.Add(user);
            await _db.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
