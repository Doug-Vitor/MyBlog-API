public interface IBaseRepository<TModel, TEntity> where TEntity : BaseEntity
{
    Task InsertAsync(TModel entity);
    Task<TEntity> GetByIdAsync(int? id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task UpdateAsync(int? id, TModel entity);
    Task RemoveAsync(int? id);
}
