public class LoginResultDTO
{
    public string Username { get; set; }
    public string Token { get; set; }

    public LoginResultDTO()
    {
    }

    public LoginResultDTO(string username, string token) => (Username, Token) = (username, token);
}
