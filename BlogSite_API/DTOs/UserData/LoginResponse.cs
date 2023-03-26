using BlogSite_API.Models;

namespace BlogSite_API.DTOs.UserData
{
    public class LoginResponse
    {
        public LocalUser User { get; set; }
        public string Token { get; set; }
    }
}
