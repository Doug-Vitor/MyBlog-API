public interface IRoleRepository
{
    Task<Role> GetByNameAsync(string roleName);
    Task<IEnumerable<Role>> GetByUserIdAsync(int userId);
}
