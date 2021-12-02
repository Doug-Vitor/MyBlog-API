using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class CreateUserInputModel : BaseUserViewModel
{
    [DisplayName("Senha")]
    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    [DataType(DataType.Password)]
    [PasswordValidation]
    public string Password { get; set; }

    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public CreateUserInputModel()
    {
    }

    public CreateUserInputModel(string username, string password) : base(username) => Password = password; 

    public CreateUserInputModel(string username, string password, string email) : base(username) => 
        (Password, Email) = (password, email);
}
