using BlogSite_API.DTOs.UserData;
using BlogSite_API.Models;

namespace BlogSite_API.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginRequest> Login(LoginRequest loginRequest);
        Task<LocalUser> Register(RegisterationRequest registerationRequest);
    }
}
