public interface IUserServices
{
    Task<UserViewModel> GetByIdAsync(int id);
    Task SignUpAsync(UserInputModel inputModel);
    Task SignInAsync(UserSignInModel inputModel);
    Task UpdateAsync(int userId, UserInputModel inputModel);
}