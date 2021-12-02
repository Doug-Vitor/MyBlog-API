using BetterNews.Infrastructure.Data.Repositories.AuthenticationRepositories;
using Microsoft.EntityFrameworkCore;

public class CrossCuttingRepository : BaseRepository
{
    public CrossCuttingRepository(AuthenticationContext context) : base(context)
    {
    }

    public async Task<int> GetRoleIdByNameAsync(string roleName) => 
        (await Context.Roles.FirstOrDefaultAsync(prop => prop.Name == roleName)).Id;

    public async Task<IEnumerable<int>> GetUserRolesIdByUserIdAsync(int? userId) =>
        (await Context.UsersRoles.Where(prop => prop.UserId == userId.Value).ToListAsync())
        .Select(prop => prop.RoleId);
}