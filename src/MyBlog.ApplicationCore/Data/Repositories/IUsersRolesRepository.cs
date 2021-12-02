public interface IUsersRolesRepository
{
    Task InsertDefaultAsync(int? userId);
    Task InsertAsync(int? userId, int roleId);
    Task<IEnumerable<UsersRoles>> GetByUserIdAsync(int userId);
}
