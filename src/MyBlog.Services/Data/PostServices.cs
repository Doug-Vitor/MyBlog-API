using Ardalis.GuardClauses;
using AutoMapper;

public class PostServices : IPostServices
{
    private readonly IBaseRepository<Post> _postRepository;
    private readonly IUserServices _userServices;
    private readonly IMapper _mapper;
    private readonly HttpContextAccessorHelper _contextAccessor;

    public PostServices(IBaseRepository<Post> postRepository, IUserServices userServices, HttpContextAccessorHelper contextAccessor, IMapper mapper) => 
        (_postRepository, _userServices, _contextAccessor, _mapper) = (postRepository, userServices, contextAccessor, mapper);

    public async Task<int> InsertAsync(CreatePostInputModel createdPost)
    {
        if (createdPost.CreatedAt == default || !createdPost.CreatedAt.HasValue)
            createdPost.CreatedAt = DateTime.UtcNow;

        createdPost.EnsureFieldsIsValid();

        Post post = _mapper.Map<Post>(createdPost);
        post.AuthorId = _contextAccessor.GetAuthenticatedUserId().GetValueOrDefault();

        await _postRepository.InsertAsync(post);
        return post.Id;
    }

    public async Task<PostDTO> GetByIdAsync(int? id)
    {
        Guard.Against.Null(id, nameof(id), "Campo ID não pode ser vazio.");
        PostDTO post = _mapper.Map<PostDTO>(await _postRepository.GetByIdAsync(id.GetValueOrDefault()));
        post.AuthorUsername = await _userServices.GetUserNameByIdAsync(post.AuthorId);

        return post;
    }

    public async Task<IEnumerable<PostDTO>> GetAllAsync()
    {
        IEnumerable<PostDTO> posts = _mapper.Map<IEnumerable<PostDTO>>(await _postRepository.GetAllAsync());
        foreach (PostDTO post in posts)
            post.AuthorUsername = await _userServices.GetUserNameByIdAsync(post.AuthorId);

        return posts;
    }

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