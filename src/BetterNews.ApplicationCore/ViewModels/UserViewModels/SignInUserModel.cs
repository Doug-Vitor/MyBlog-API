using System.ComponentModel.DataAnnotations;

public class SignInUserModel
{
    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    public string Username_Email { get; set; }

    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public SignInUserModel()
    {
    }

    public SignInUserModel(string username_email, string password) => (Username_Email, Password) = (username_email, password);
}
