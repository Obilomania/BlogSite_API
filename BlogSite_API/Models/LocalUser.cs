namespace BlogSite_API.Models
{
    public class LocalUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
