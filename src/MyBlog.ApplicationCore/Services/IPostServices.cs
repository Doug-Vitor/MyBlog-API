public interface IPostServices
{
    Task<int> InsertAsync(CreatePostInputModel createdPost);
    Task<PostDTO> GetByIdAsync(int? id);
    Task<IEnumerable<PostDTO>> GetAllAsync();
    Task UpdateAsync(int? id, CreatePostInputModel updatedPost);
    Task RemoveAsync(int? id);
}