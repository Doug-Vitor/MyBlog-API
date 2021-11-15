using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AuthenticationContext _context;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public UserRepository(AuthenticationContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

    public Task<User> GetByIdAsync(int id) => _context.Users.FirstOrDefaultAsync(prop => prop.Id == id) ?? throw new NotFoundException("Não foi possível encontrar um usuário correspondente ao ID fornecido.");

    public async Task SignUpAsync(UserInputModel inputModel)
    {
        User user = _mapper.Map<User>(inputModel);

        if (_context.Users.Any(prop => prop.Username == user.Username)) throw new FieldInUseException(nameof(user.Username));
        if (_context.Users.Any(prop => prop.Email == user.Email)) throw new FieldInUseException(nameof(user.Email));

        Role userRole = await _roleRepository.GetByNameAsync("User");
        user.AddRole(userRole.Id);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task SignInAsync(UserSignInModel signInModel)
    {
        bool result = await _context.Users.AnyAsync(prop => prop.Username == signInModel.Username_Email || prop.Email == signInModel.Username_Email && prop.PasswordHash == signInModel.Password);
        if (result is false) throw new SignInFailException();
    }

    public async Task UpdateAsync(int userId, UserInputModel inputModel)
    {
        User user = _mapper.Map<User>(inputModel);

        user.Id = userId;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}