using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AuthenticationContext _context;
    private readonly IUsersRolesRepository _usersRolesRepository;

    public UserRepository(AuthenticationContext context, IUsersRolesRepository usersRolesRepository) => 
        (_context, _usersRolesRepository) = (context, usersRolesRepository);

    public Task<User> GetByIdAsync(int id) => _context.Users.FirstOrDefaultAsync(prop => prop.Id == id) ?? 
        throw new NotFoundException("Não foi possível encontrar um usuário correspondente ao ID fornecido.");

    public async Task<int> SignUpAsync(User user)
    {
        if (_context.Users.Any(prop => prop.Username == user.Username)) 
            throw new FieldInUseException("Nome de usuário");
        if (_context.Users.Any(prop => prop.Email == user.Email)) 
            throw new FieldInUseException(nameof(user.Email));

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        await _usersRolesRepository.InsertDefaultAsync(user.Id);

        return user.Id;
    }

    public async Task<int?> SignInAsync(SignInUserModel signInModel) => 
        (await _context.Users.FirstOrDefaultAsync(prop => prop.Username == signInModel.Username && 
        prop.PasswordHash == signInModel.Password.ToHash()))?.Id ?? throw new SignInFailException();

    public async Task UpdateAsync(int userId, User user)
    {
        user.Id = userId;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}