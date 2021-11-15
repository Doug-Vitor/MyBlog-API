using Microsoft.EntityFrameworkCore;

public class RoleRepository : IRoleRepository
{
    private readonly AuthenticationContext _context;

    public RoleRepository(AuthenticationContext context) => _context = context;

    public async Task<Role> GetByNameAsync(string roleName) => await _context.Roles.FirstOrDefaultAsync(prop => prop.Name == roleName);

    public async Task<IEnumerable<Role>> GetByUserIdAsync(int userId)
    {
        IEnumerable<UsersRoles> usersRoles = await _context.UsersRoles.Where(userProp => userProp.UserId == userId).ToListAsync();

        List<Role> roles = new();
        foreach (UsersRoles userRole in usersRoles) 
            roles.Add(await _context.Roles.FirstOrDefaultAsync(prop => prop.Id == userRole.RoleId));

        return roles;
    }
}