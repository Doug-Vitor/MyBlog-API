using Ardalis.GuardClauses;
using AutoMapper;

public class UserServices : IUserServices
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly HttpContextAccessorHelper _contextAccessor;

    public UserServices(IUserRepository userRepository, IMapper mapper, HttpContextAccessorHelper contextAccessor)
        => (_userRepository, _mapper, _contextAccessor) = (userRepository, mapper, contextAccessor);

    public async Task<UserDTO> GetByIdAsync(int? id)
    {
        Guard.Against.Null(id, nameof(id), "Campo ID não pode ser vazio.");
        return _mapper.Map<UserDTO>(await _userRepository.GetByIdAsync(id.Value)) ?? 
            throw new NotFoundException("Não foi possível encontrar um usuário correspondente ao ID fornecido.");
    }

    public async Task<string> GetUserNameByIdAsync(int? id) => (await GetByIdAsync(id)).Username;

    public async Task<int> SignUpAsync(CreateUserInputModel inputModel)
    {
        inputModel.EnsureFieldsIsValid();
        return await _userRepository.SignUpAsync(MapToUser(inputModel));
    }

    public async Task<int?> SignInAsync(SignInUserModel signInModel)
    {
        signInModel.EnsureFieldsIsValid();
        return await _userRepository.SignInAsync(signInModel);
    }
    public async Task<UserDTO> GetAuthenticatedUserAsync()
        => _mapper.Map<UserDTO>(await _userRepository.GetByIdAsync(_contextAccessor.GetAuthenticatedUserId().GetValueOrDefault()));

    public async Task UpdateAuthenticatedUserAsync(CreateUserInputModel inputModel)
    {
        if (string.IsNullOrWhiteSpace(inputModel.Password))
            inputModel.Password = (await _userRepository.GetByIdAsync(_contextAccessor.GetAuthenticatedUserId().GetValueOrDefault())).PasswordHash;
        inputModel.EnsureFieldsIsValid();
        
        await _userRepository.UpdateAsync(_contextAccessor.GetAuthenticatedUserId().GetValueOrDefault(), MapToUser(inputModel));
    }

    private User MapToUser(CreateUserInputModel inputModel)
    {
        User user = _mapper.Map<User>(inputModel);
        user.PasswordHash = inputModel.Password.ToHash();
        return user;
    }
}
