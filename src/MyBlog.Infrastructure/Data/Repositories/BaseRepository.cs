using Microsoft.EntityFrameworkCore;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly CoreContext _context;

    public BaseRepository(CoreContext context) => _context = context;

    public async Task InsertAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

    public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FirstOrDefaultAsync(prop => prop.Id == id);

    public async Task UpdateAsync(int id, T entity)
    {
        entity.Id = id;
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        _context.Remove(await GetByIdAsync(id));
        await _context.SaveChangesAsync();
    }
}