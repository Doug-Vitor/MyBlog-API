using System.ComponentModel.DataAnnotations;

public class UserViewModel
{
    public string Username { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public UserViewModel()
    {
    }

    public UserViewModel(string username, string email) => (Username, Email) = (username, email);
}
