public interface IPostServices
{
    Task InsertAsync(Post post);
    Task<Post> GetByIdAsync(int? id);
    Task<IEnumerable<Post>> GetAllAsync();
    Task UpdateAsync(int? id, Post post);
    Task RemoveAsync(int? id);
}