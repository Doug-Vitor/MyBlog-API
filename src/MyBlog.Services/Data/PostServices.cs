using Ardalis.GuardClauses;

public class PostServices : IPostServices
{
    private readonly IBaseRepository<Post> _postRepository;

    public PostServices(IBaseRepository<Post> postRepository) => _postRepository = postRepository;

    public async Task InsertAsync(Post post)
    {
        Guard.Against.Null(post, nameof(post), "Não é possível criar uma publicação vazia.");
        Guard.Against.Null(post.Content, nameof(post.Content), "Não é possível criar uma publicação vazia.");
        await _postRepository.InsertAsync(post);
    }

    public async Task<Post> GetByIdAsync(int? id)
    {
        Guard.Against.Null(id, nameof(id), "Campo ID não pode ser vazio.");
        return await _postRepository.GetByIdAsync(id.GetValueOrDefault()) ?? throw new NotFoundException("Não foi possível encontrar uma publicação correspondente ao ID fornecido.");
    }

    public async Task<IEnumerable<Post>> GetAllAsync() => await _postRepository.GetAllAsync();

    public async Task UpdateAsync(int? id, Post post)
    {
        Guard.Against.Null(id, nameof(id), "Campo ID não pode ser vazio.");
        Guard.Against.Null(post, nameof(post), "Não é possível criar uma publicação vazia.");
        Guard.Against.Null(post.Content, nameof(post.Content), "Insira um texto válido na publicação.");
        await _postRepository.UpdateAsync(id.GetValueOrDefault(), post);
    }

    public Task RemoveAsync(int? id)
    {
        throw new NotImplementedException();
    }
}