namespace TaskManagementSystem.Authoriz
{
    public class User
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
    public static class DummyUsers
    {
        public static List<User> Users = new()
        {
            new User
            {
                Username = "Admin",
                Password = "123",
                Role = "Admin"
            },
            new User
            {
                Username = "User",
                Password = "123",
                Role = "User"
            }
        };
    }
}
