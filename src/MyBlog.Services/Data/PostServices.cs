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
        createdPost.EnsureFieldsIsValid();

        Post post = _mapper.Map<Post>(createdPost);
        post.AuthorId = _contextAccessor.GetAuthenticatedUserId().GetValueOrDefault();

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
        updatedPost.EnsureFieldsIsValid();

        int authenticatedUserId = _contextAccessor.GetAuthenticatedUserId().GetValueOrDefault();
        Post post = await _postRepository.GetByIdAsync(id.GetValueOrDefault());
        if (post.AuthorId == authenticatedUserId)
        {
            post.Content = updatedPost.Content;
            await _postRepository.UpdateAsync(id.GetValueOrDefault(), post);
        }
        else throw new UnauthorizedAccessException("Você não está autorizado a executar essa ação.");
    }

    public async Task RemoveAsync(int? id)
    {
        Guard.Against.Null(id, nameof(id), "Campo ID não pode ser vazio.");

        Post post = await _postRepository.GetByIdAsync(id.GetValueOrDefault());
        if (post.AuthorId == _contextAccessor.GetAuthenticatedUserId().GetValueOrDefault()) 
            await _postRepository.RemoveAsync(id.GetValueOrDefault());
        else throw new UnauthorizedAccessException("Você não está autorizado a executar essa ação.");

    }
}