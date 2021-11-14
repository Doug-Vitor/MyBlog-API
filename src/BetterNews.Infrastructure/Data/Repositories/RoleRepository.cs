using Microsoft.EntityFrameworkCore;

public class RoleRepository : IRoleRepository
{
    private readonly AuthenticationContext _context;

    public RoleRepository(AuthenticationContext context) => _context = context;

    public async Task<IEnumerable<Role>> GetByUserIdAsync(int userId)
    {
        IEnumerable<UsersRoles> usersroles = await _context.UserRoles.Where(userProp => userProp.UserId == userId).ToListAsync();

        List<Role> roles = new();
        foreach (UsersRoles userRole in usersroles) 
            roles.Add(await _context.Roles.FirstOrDefaultAsync(prop => prop.Id == userRole.RoleId));

        return roles;
    }
}