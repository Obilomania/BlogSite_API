namespace BlogSite_API.DTOs.AuthenticationDTOs
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
