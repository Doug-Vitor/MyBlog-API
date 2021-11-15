public class UserInputModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public UserInputModel()
    {
    }

    public UserInputModel(string username, string password) => (Username, Password) = (username, password); 

    public UserInputModel(string username, string password, string email) => (Username, Password, Email) = (username, password, email);
}
