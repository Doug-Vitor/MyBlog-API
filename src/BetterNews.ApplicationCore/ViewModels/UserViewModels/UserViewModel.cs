using System.ComponentModel.DataAnnotations;

public class UserViewModel : BaseUserViewModel
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public UserViewModel()
    {
    }

    public UserViewModel(string username, string email) : base(username) => Email = email;
}
