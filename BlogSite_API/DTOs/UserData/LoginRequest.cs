namespace BlogSite_API.DTOs.UserData
{
    public class LoginRequest
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
