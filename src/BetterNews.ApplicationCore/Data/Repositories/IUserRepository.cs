public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task SignUpAsync(CreateUserInputModel inputModel);
    Task SignInAsync(SignInUserModel inputModel);
    Task UpdateAsync(int userId, CreateUserInputModel inputModel);
}
