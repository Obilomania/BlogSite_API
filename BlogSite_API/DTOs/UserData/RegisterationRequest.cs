namespace BlogSite_API.DTOs.UserData
{
    public class RegisterationRequest
    {
        public string Name { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
