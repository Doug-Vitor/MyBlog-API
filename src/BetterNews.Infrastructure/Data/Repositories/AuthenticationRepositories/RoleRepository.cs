using BetterNews.Infrastructure.Data.Repositories.AuthenticationRepositories;
using Microsoft.EntityFrameworkCore;

public class RoleRepository : BaseRepository, IRoleRepository
{
    private readonly IUsersRolesRepository _usersRolesRepository;

    public RoleRepository(AuthenticationContext context, IUsersRolesRepository usersRolesRepository) : base(context) => _usersRolesRepository = usersRolesRepository;

    public async Task<Role> GetByIdAsync(int id) => await Context.Roles.FirstOrDefaultAsync(prop => prop.Id == id);

    public async Task<Role> GetByNameAsync(string roleName) => await Context.Roles.FirstOrDefaultAsync(prop => prop.Name == roleName);

    public async Task<IEnumerable<Role>> GetByUserIdAsync(int userId)
    {
        IEnumerable<int> usersRolesId = (await _usersRolesRepository.GetByUserIdAsync(userId)).Select(prop => prop.RoleId);

        List<Role> roles = new();
        foreach (int userRoleId in usersRolesId)
            roles.Add(await GetByIdAsync(userRoleId));

        return roles;
    }
}