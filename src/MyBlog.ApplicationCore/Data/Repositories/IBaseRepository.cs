public interface IBaseRepository<T> where T : BaseEntity
{
    Task InsertAsync(T entity);
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task UpdateAsync(int id, T entity);
    Task RemoveAsync(int id);
}
