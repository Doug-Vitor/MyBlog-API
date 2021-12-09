using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class SignInUserModel : BaseUserDTO
{
    [DisplayName("Senha")]
    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public SignInUserModel()
    {
    }

    public SignInUserModel(string username, string password) : base(username) => Password = password;
}
