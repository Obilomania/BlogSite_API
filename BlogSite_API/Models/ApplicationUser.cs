using Microsoft.AspNetCore.Identity;

namespace BlogSite_API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NickName { get; set; }
    }
}
