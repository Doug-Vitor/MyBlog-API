using Ardalis.GuardClauses;

public class PostServices : IPostServices
{
    private readonly IBaseRepository<Post> _postRepository;
    private readonly HttpContextAccessorHelper _contextAccessor;

    public PostServices(IBaseRepository<Post> postRepository, HttpContextAccessorHelper contextAccessor) => 
        (_postRepository, _contextAccessor) = (postRepository, contextAccessor);

    public async Task InsertAsync(Post post)
    {
        EnsurePropertiesIsValid(post);
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
        EnsurePropertiesIsValid(post);

        if (post.UserId != _contextAccessor.GetAuthenticatedUserId().GetValueOrDefault())
            throw new UnauthorizedAccessException("Você não está autorizado a executar essa ação.");

        await _postRepository.UpdateAsync(id.GetValueOrDefault(), post);
    }

    public async Task RemoveAsync(int? id)
    {
        Guard.Against.Null(id, nameof(id), "Campo ID não pode ser vazio.");
        Post post = await GetByIdAsync(id);
        EnsurePropertiesIsValid(post);

        if (post.UserId != _contextAccessor.GetAuthenticatedUserId().GetValueOrDefault())
            throw new UnauthorizedAccessException("Você não está autorizado a executar essa ação.");

        await _postRepository.RemoveAsync(id.GetValueOrDefault());
    }

    private void EnsurePropertiesIsValid(Post post)
    {
        Guard.Against.Null(post, nameof(post), "Não é possível criar uma publicação vazia.");
        Guard.Against.Null(post.Content, nameof(post.Content), "Insira um texto válido na publicação.");
    }
}