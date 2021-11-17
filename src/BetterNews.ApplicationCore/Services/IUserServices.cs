public interface IUserServices
{
    Task<UserViewModel> GetByIdAsync(int id);
    Task SignUpAsync(CreateUserInputModel inputModel);
    Task SignInAsync(SignInUserModel inputModel);
    Task UpdateAsync(int userId, CreateUserInputModel inputModel);
}