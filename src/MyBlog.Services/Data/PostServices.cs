using Ardalis.GuardClauses;
using AutoMapper;

public class PostServices : IPostServices
{
    private readonly IBaseRepository<Post> _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly HttpContextAccessorHelper _contextAccessor;

    public PostServices(IBaseRepository<Post> postRepository, IUserRepository userRepository, HttpContextAccessorHelper contextAccessor, IMapper mapper) => 
        (_postRepository, _userRepository, _contextAccessor, _mapper) = (postRepository, userRepository, contextAccessor, mapper);

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
        Post post = await _postRepository.GetByIdAsync(id.GetValueOrDefault());

        PostDTO mappedPost = _mapper.Map<PostDTO>(post);
        mappedPost.AuthorInfos = _mapper.Map<PostInteractorsDTO>(await _userRepository.GetByIdAsync(post.AuthorId));
        return mappedPost;
    }

    public async Task<IEnumerable<PostDTO>> GetAllAsync()
    {
        IEnumerable<Post> posts = await _postRepository.GetAllAsync();

        IEnumerable<PostDTO> mappedPosts = _mapper.Map<IEnumerable<PostDTO>>(posts);
        for (int count = 0; count < posts.Count(); count++)
            mappedPosts.ElementAtOrDefault(count).AuthorInfos = _mapper.Map<PostInteractorsDTO>(await _userRepository.GetByIdAsync(posts.ElementAtOrDefault(count).AuthorId));

        return mappedPosts;
    }

    public async Task UpdateAsync(int? id, CreatePostInputModel updatedPost)
    {
        Guard.Against.Null(id, nameof(id), "Campo ID não pode ser vazio.");
        updatedPost.EnsureFieldsIsValid();

        Post post = await _postRepository.GetByIdAsync(id.GetValueOrDefault());
        if (post.AuthorId == _contextAccessor.GetAuthenticatedUserId().GetValueOrDefault())
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