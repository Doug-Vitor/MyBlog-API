public class UserViewModel
{
    public string Username { get; set; }
    public string Email { get; set; }

    public UserViewModel()
    {
    }

    public UserViewModel(string username, string email) => (Username, Email) = (username, email);
}
