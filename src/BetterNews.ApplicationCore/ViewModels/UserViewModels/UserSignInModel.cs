public class UserSignInModel
{
    public string Username_Email { get; set; }
    public string Password { get; set; }

    public UserSignInModel()
    {
    }

    public UserSignInModel(string username_email, string password) => (Username_Email, Password) = (username_email, password);
}
