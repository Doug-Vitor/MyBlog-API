public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<int> SignUpAsync(User user);
    Task<int?> SignInAsync(SignInUserModel inputModel);
    Task UpdateAsync(int userId, User user);
}
