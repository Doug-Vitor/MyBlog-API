using Ardalis.GuardClauses;
using AutoMapper;

public class PostServices : IPostServices
{
    private readonly IBaseRepository<Post> _postRepository;
    private readonly IMapper _mapper;
    private readonly HttpContextAccessorHelper _contextAccessor;

    public PostServices(IBaseRepository<Post> postRepository, HttpContextAccessorHelper contextAccessor, IMapper mapper) => 
        (_postRepository, _contextAccessor, _mapper) = (postRepository, contextAccessor, mapper);

    public async Task<int> InsertAsync(CreatePostInputModel createdPost)
    {
        EnsurePropertiesIsValid(createdPost);

        Post post = _mapper.Map<Post>(createdPost);
        post.UserId = _contextAccessor.GetAuthenticatedUserId().GetValueOrDefault();

        await _postRepository.InsertAsync(post);
        return post.Id;
    }

    public async Task<PostDTO> GetByIdAsync(int? id)
    {
        Guard.Against.Null(id, nameof(id), "Campo ID não pode ser vazio.");
        return _mapper.Map<PostDTO>(await _postRepository.GetByIdAsync(id.GetValueOrDefault()));
    }

    public async Task<IEnumerable<PostDTO>> GetAllAsync() => _mapper.Map<IEnumerable<PostDTO>>(await _postRepository.GetAllAsync());

    public async Task UpdateAsync(int? id, CreatePostInputModel updatedPost)
    {
        Guard.Against.Null(id, nameof(id), "Campo ID não pode ser vazio.");
        EnsurePropertiesIsValid(updatedPost);

        int authenticatedUserId = _contextAccessor.GetAuthenticatedUserId().GetValueOrDefault();
        if ((await _postRepository.GetByIdAsync(id.GetValueOrDefault())).UserId == authenticatedUserId)
        {
            Post post = _mapper.Map<Post>(updatedPost);
            post.UserId = authenticatedUserId;
            await _postRepository.UpdateAsync(id.GetValueOrDefault(), post);
        }
        else throw new UnauthorizedAccessException("Você não está autorizado a executar essa ação.");
    }

    public async Task RemoveAsync(int? id)
    {
        Guard.Against.Null(id, nameof(id), "Campo ID não pode ser vazio.");

        Post post = await _postRepository.GetByIdAsync(id.GetValueOrDefault());
        if (post.UserId == _contextAccessor.GetAuthenticatedUserId().GetValueOrDefault()) 
            await _postRepository.RemoveAsync(id.GetValueOrDefault());
        else throw new UnauthorizedAccessException("Você não está autorizado a executar essa ação.");

    }

    private void EnsurePropertiesIsValid(CreatePostInputModel createdPost)
    {
        Guard.Against.Null(createdPost, nameof(createdPost), "Não é possível criar ou tornar uma publicação vazia.");
        Guard.Against.Null(createdPost.Content, nameof(createdPost.Content), "Insira um texto válido na publicação.");
    }
}