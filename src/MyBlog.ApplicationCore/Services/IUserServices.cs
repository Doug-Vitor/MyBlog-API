public interface IUserServices
{
    Task<UserDTO> GetByIdAsync(int? id);
    Task<string> GetUserNameByIdAsync(int? id);
    Task<int> SignUpAsync(CreateUserInputModel inputModel);
    Task<int?> SignInAsync(SignInUserModel inputModel);
    Task<UserDTO> GetAuthenticatedUserAsync();
    Task UpdateAuthenticatedUserAsync(CreateUserInputModel inputModel);
}