using Ardalis.GuardClauses;
using AutoMapper;

public class UserServices : IUserServices
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly HttpContextAccessorHelper _contextAccessor;

    public UserServices(IUserRepository userRepository, IMapper mapper, HttpContextAccessorHelper contextAccessor) => (_userRepository, _mapper, _contextAccessor) = (userRepository, mapper, contextAccessor);

    public async Task<UserViewModel> GetByIdAsync(int id)
    {
        Guard.Against.Null(id, nameof(id), "Campo ID não pode ser vazio.");
        return _mapper.Map<UserViewModel>(await _userRepository.GetByIdAsync(id)) ?? throw new NotFoundException("Não foi possível encontrar um usuário correspondente ao ID fornecido.");
    }

    public async Task SignUpAsync(CreateUserInputModel inputModel)
    {
        Guard.Against.Null(inputModel, nameof(inputModel), "Por favor, insira dados válidos.");
        Guard.Against.Null(inputModel.Username, nameof(inputModel.Username), "Por favor, insira um nome de usuário válido.");
        Guard.Against.Null(inputModel.Email, nameof(inputModel.Email), "Por favor, insira um e-mail válido.");
        Guard.Against.Null(inputModel.Password, nameof(inputModel.Password), "Por favor, insira uma senha válida.");

        inputModel.Password = inputModel.Password.ToHash();
        await _userRepository.SignUpAsync(inputModel);
    }

    public async Task SignInAsync(SignInUserModel signInModel)
    {
        Guard.Against.Null(signInModel, nameof(signInModel), "Por favor, insira dados válidos.");
        Guard.Against.Null(signInModel.Username_Email, nameof(signInModel.Username_Email), "Por favor, insira um nome de usuário/e-mail válido.");
        Guard.Against.Null(signInModel.Password, nameof(signInModel.Password), "Por favor, insira uma senha válida.");

        signInModel.Password = signInModel.Password.ToHash();
        await _userRepository.SignInAsync(signInModel);
    }

    public async Task UpdateAsync(int userId, CreateUserInputModel inputModel)
    {
        if (_contextAccessor.GetAuthenticatedUserId() == userId) await _userRepository.UpdateAsync(userId, inputModel);
        else throw new UnauthorizedAccessException("Você não possui permissão para acessar esse conteúdo.");
    }
}
