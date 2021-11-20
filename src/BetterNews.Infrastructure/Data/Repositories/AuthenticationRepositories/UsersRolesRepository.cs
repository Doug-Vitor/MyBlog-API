using BetterNews.Infrastructure.Data.Repositories.AuthenticationRepositories;
using Microsoft.EntityFrameworkCore;

public class UsersRolesRepository : BaseRepository, IUsersRolesRepository
{
    private readonly CrossCuttingRepository _crossCuttingRepository;

    public UsersRolesRepository(AuthenticationContext context, CrossCuttingRepository crossCuttingRepository) : base(context) => _crossCuttingRepository = crossCuttingRepository;

    public async Task<IEnumerable<UsersRoles>> GetByUserIdAsync(int userId) => await Context.UsersRoles.Where(prop => prop.UserId == userId).ToListAsync();

    public async Task InsertDefaultAsync(int? userId)
    {
        await Context.UsersRoles.AddAsync(new UsersRoles(userId.Value, await _crossCuttingRepository.GetRoleIdByNameAsync("User")));
        await Context.SaveChangesAsync();
    }

    public async Task InsertAsync(int? userId, int roleId)
    {
        await Context.UsersRoles.AddAsync(new UsersRoles(userId.Value, roleId));
        await Context.SaveChangesAsync();
    }
}