using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenServices : ITokenServices
{
    private readonly IUserServices _userServices;
    private readonly IRoleRepository _roleRepository;
    private readonly SecretsConfiguration _secretsConfiguration;

    public TokenServices(IUserServices userServices, IRoleRepository roleRepository, IOptions<SecretsConfiguration> secretsConfiguration) =>
        (_userServices, _roleRepository, _secretsConfiguration) = (userServices, roleRepository, secretsConfiguration.Value);

    public async Task<string> GenerateTokenAsync(int? userId)
    {
        UserViewModel userViewModel = await _userServices.GetByIdAsync(userId);

        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.ASCII.GetBytes(_secretsConfiguration.Secret);

        List<Claim> claims = new()
        {
            new(ClaimTypes.Name, userViewModel.Username),
            new(ClaimTypes.Email, userViewModel.Email),
            new(ClaimTypes.NameIdentifier, userId.Value.ToString())
        };

        IEnumerable<Role> userRoles = await _roleRepository.GetByUserIdAsync(userId);
        foreach (Role role in userRoles)
            claims.Add(new(ClaimTypes.Role, role.Name));

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(12),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}