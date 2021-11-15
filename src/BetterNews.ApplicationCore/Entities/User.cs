using System.ComponentModel.DataAnnotations.Schema;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }

    [NotMapped]
    public ICollection<int> RolesId { get; set; }

    public User()
    {
    }

    public User(string username, string passwordHash, string email, ICollection<int> rolesId)
        => (Username, PasswordHash, Email, RolesId) = (username, passwordHash, email, rolesId);

    public void AddRole(int roleId) => RolesId.Add(roleId);
}
