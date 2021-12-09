using System.ComponentModel.DataAnnotations;

public class UserDTO : BaseUserDTO
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public UserDTO()
    {
    }

    public UserDTO(string username, string email) : base(username) => Email = email;
}
