using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public abstract class BaseUserDTO
{
    [DisplayName("Nome de usuário")]
    [Required(ErrorMessage = "Campo {0} é obrigatório.")]
    [StringLength(75, MinimumLength = 5, ErrorMessage = "Campo {0} deve conter entre {2} e {1} caracteres.")]
    public string Username { get; set; }

    public BaseUserDTO()
    {
    }

    public BaseUserDTO(string username) => Username = username;
}
