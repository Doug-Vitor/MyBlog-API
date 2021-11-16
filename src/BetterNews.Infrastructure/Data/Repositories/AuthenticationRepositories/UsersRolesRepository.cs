using BetterNews.Infrastructure.Data.Repositories.AuthenticationRepositories;
using Microsoft.EntityFrameworkCore;

public class UsersRolesRepository : BaseRepository, IUsersRolesRepository
{
    private readonly IRoleRepository _roleRepository;

    public UsersRolesRepository(AuthenticationContext context, IRoleRepository roleRepository) : base(context) => _roleRepository = roleRepository;

    public async Task<IEnumerable<UsersRoles>> GetByUserIdAsync(int userId) => await Context.UsersRoles.Where(prop => prop.UserId == userId).ToListAsync();

    public async Task InsertDefaultAsync(int userId)
    {
        await Context.UsersRoles.AddAsync(new UsersRoles(userId, (await _roleRepository.GetByNameAsync("User")).Id));
        await Context.SaveChangesAsync();
    }

    public async Task InsertAsync(int userId, int roleId)
    {
        await Context.UsersRoles.AddAsync(new UsersRoles(userId, roleId));
        await Context.SaveChangesAsync();
    }
}