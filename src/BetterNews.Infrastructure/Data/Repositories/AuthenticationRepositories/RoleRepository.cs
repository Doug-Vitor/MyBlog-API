using BetterNews.Infrastructure.Data.Repositories.AuthenticationRepositories;
using Microsoft.EntityFrameworkCore;

public class RoleRepository : BaseRepository, IRoleRepository
{
    private readonly CrossCuttingRepository _crossCuttingRepository;

    public RoleRepository(AuthenticationContext context, CrossCuttingRepository crossCuttingRepository) : base(context) => _crossCuttingRepository = crossCuttingRepository;

    public async Task<Role> GetByIdAsync(int? id) => await Context.Roles.FirstOrDefaultAsync(prop => prop.Id == id);

    public async Task<Role> GetByNameAsync(string roleName) => await Context.Roles.FirstOrDefaultAsync(prop => prop.Name == roleName);

    public async Task<IEnumerable<Role>> GetByUserIdAsync(int? userId)
    {
        List<Role> roles = new();
        foreach (int userRoleId in await _crossCuttingRepository.GetUserRolesIdByUserIdAsync(userId))
            roles.Add(await GetByIdAsync(userRoleId));

        return roles;
    }
}