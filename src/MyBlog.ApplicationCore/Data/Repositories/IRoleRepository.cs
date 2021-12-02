public interface IRoleRepository
{
    Task<Role> GetByIdAsync(int? id);
    Task<Role> GetByNameAsync(string roleName);
    Task<IEnumerable<Role>> GetByUserIdAsync(int? userId);
}
