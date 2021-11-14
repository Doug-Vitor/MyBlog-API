public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetByUserIdAsync(int userId);
}
