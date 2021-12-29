public class User : BaseEntity
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }

    public User()
    {
    }

    public User(string username, string passwordHash, string email)
        => (Username, PasswordHash, Email) = (username, passwordHash, email);
}
