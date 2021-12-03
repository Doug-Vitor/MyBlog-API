using Ardalis.GuardClauses;
using AutoMapper;

public class UserServices : IUserServices
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly HttpContextAccessorHelper _contextAccessor;

    public UserServices(IUserRepository userRepository, IMapper mapper, HttpContextAccessorHelper contextAccessor)
        => (_userRepository, _mapper, _contextAccessor) = (userRepository, mapper, contextAccessor);

    public async Task<UserViewModel> GetByIdAsync(int? id)
    {
        Guard.Against.Null(id, nameof(id), "Campo ID não pode ser vazio.");
        return _mapper.Map<UserViewModel>(await _userRepository.GetByIdAsync(id.Value)) ?? 
            throw new NotFoundException("Não foi possível encontrar um usuário correspondente ao ID fornecido.");
    }

    public async Task<int> SignUpAsync(CreateUserInputModel inputModel)
    {
        Guard.Against.Null(inputModel, nameof(inputModel), "Por favor, insira dados válidos.");
        Guard.Against.Null(inputModel.Username, nameof(inputModel.Username), "Por favor, insira um nome de usuário válido.");
        Guard.Against.Null(inputModel.Email, nameof(inputModel.Email), "Por favor, insira um e-mail válido.");
        Guard.Against.Null(inputModel.Password, nameof(inputModel.Password), "Por favor, insira uma senha válida.");
        return await _userRepository.SignUpAsync(MapToUser(inputModel));
    }

    public async Task<int?> SignInAsync(SignInUserModel signInModel)
    {
        Guard.Against.Null(signInModel, nameof(signInModel), "Por favor, insira dados válidos.");
        Guard.Against.Null(signInModel.Username, nameof(signInModel.Username), "Por favor, insira um nome de usuário válido.");
        Guard.Against.Null(signInModel.Password, nameof(signInModel.Password), "Por favor, insira uma senha válida.");
        return await _userRepository.SignInAsync(signInModel);
    }
    public async Task<UserViewModel> GetAuthenticatedUserAsync()
        => _mapper.Map<UserViewModel>(await _userRepository.GetByIdAsync(_contextAccessor.GetAuthenticatedUserId().GetValueOrDefault()));

    public async Task UpdateAuthenticatedUserAsync(CreateUserInputModel inputModel)
    {
        Guard.Against.Null(inputModel, nameof(inputModel), "Por favor, insira dados válidos.");
        Guard.Against.Null(inputModel.Username, nameof(inputModel.Username), "Por favor, insira um nome de usuário válido.");
        Guard.Against.Null(inputModel.Email, nameof(inputModel.Email), "Por favor, insira um e-mail válido.");
        Guard.Against.Null(inputModel.Password, nameof(inputModel.Password), "Por favor, insira uma senha válida.");
        
        await _userRepository.UpdateAsync(_contextAccessor.GetAuthenticatedUserId().GetValueOrDefault(), MapToUser(inputModel));
    }

    private User MapToUser(CreateUserInputModel inputModel)
    {
        User user = _mapper.Map<User>(inputModel);
        user.PasswordHash = inputModel.Password.ToHash();
        return user;
    }
}
