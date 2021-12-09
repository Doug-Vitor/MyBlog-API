public class LoginResulTdto
{
    public string Username { get; set; }
    public string Token { get; set; }

    public LoginResulTdto()
    {
    }

    public LoginResulTdto(string username, string token) => (Username, Token) = (username, token);
}
