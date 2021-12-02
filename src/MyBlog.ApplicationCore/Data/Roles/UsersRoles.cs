public class UsersRoles : BaseEntity
{
    public int UserId { get; set; }
    public int RoleId { get; set; }

    public UsersRoles()
    {
    }

    public UsersRoles(int userId, int roleId) => (UserId, RoleId) = (userId, roleId);
}
