public class LoginResultViewModel
{
    public string Username { get; set; }
    public string Token { get; set; }

    public LoginResultViewModel()
    {
    }

    public LoginResultViewModel(string username, string token) => (Username, Token) = (username, token);
}
