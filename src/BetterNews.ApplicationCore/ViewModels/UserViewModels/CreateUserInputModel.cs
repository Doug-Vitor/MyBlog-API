using System.ComponentModel.DataAnnotations;

public class CreateUserInputModel
{
    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    [StringLength(75, MinimumLength = 5, ErrorMessage = "Campo {0} deve conter entre {1} e {2} caracteres.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public CreateUserInputModel()
    {
    }

    public CreateUserInputModel(string username, string password) => (Username, Password) = (username, password); 

    public CreateUserInputModel(string username, string password, string email) => (Username, Password, Email) = (username, password, email);
}
