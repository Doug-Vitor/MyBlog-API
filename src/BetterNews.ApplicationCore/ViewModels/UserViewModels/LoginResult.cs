﻿public class LoginResult
{
    public string Username { get; set; }
    public string Token { get; set; }

    public LoginResult()
    {
    }

    public LoginResult(string username, string token) => (Username, Token) = (username, token);
}
