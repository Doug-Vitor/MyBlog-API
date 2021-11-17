using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly AuthenticationContext _context;
    private readonly IRoleRepository _roleRepository;
    private readonly IUsersRolesRepository _usersRoleRepository;
    private readonly IMapper _mapper;

    public UserRepository(AuthenticationContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);

    public Task<User> GetByIdAsync(int id) => _context.Users.FirstOrDefaultAsync(prop => prop.Id == id) ?? throw new NotFoundException("Não foi possível encontrar um usuário correspondente ao ID fornecido.");

    public async Task SignUpAsync(CreateUserInputModel inputModel)
    {
        User user = _mapper.Map<User>(inputModel);

        if (_context.Users.Any(prop => prop.Username == user.Username)) throw new FieldInUseException("Nome de usuário");
        if (_context.Users.Any(prop => prop.Email == user.Email)) throw new FieldInUseException(nameof(user.Email));


        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        int userRoleId = (await _roleRepository.GetByNameAsync("User")).Id;
        await _usersRoleRepository.InsertAsync(user.Id, userRoleId);
    }

    public async Task SignInAsync(SignInUserModel signInModel)
    {
        bool result = await _context.Users.AnyAsync(prop => prop.Username == signInModel.Username_Email || prop.Email == signInModel.Username_Email && prop.PasswordHash == signInModel.Password);
        if (result is false) throw new SignInFailException();
    }

    public async Task UpdateAsync(int userId, CreateUserInputModel inputModel)
    {
        User user = _mapper.Map<User>(inputModel);

        user.Id = userId;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}