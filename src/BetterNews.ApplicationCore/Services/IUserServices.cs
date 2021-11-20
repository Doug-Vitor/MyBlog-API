﻿public interface IUserServices
{
    Task<UserViewModel> GetByIdAsync(int? id);
    Task<int> SignUpAsync(CreateUserInputModel inputModel);
    Task<int?> SignInAsync(SignInUserModel inputModel);
    Task UpdateAsync(CreateUserInputModel inputModel);
}