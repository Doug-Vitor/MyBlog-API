using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenServices : ITokenServices
{
    private readonly IRoleRepository _roleRepository;
    private readonly SecretsConfiguration _secretsConfiguration;

    public TokenServices(IRoleRepository roleRepository, SecretsConfiguration secretsConfiguration) =>
        (_roleRepository, _secretsConfiguration) = (roleRepository, secretsConfiguration);

    public async Task<string> GenerateTokenAsync(User user)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        var key = Encoding.ASCII.GetBytes(_secretsConfiguration.Secret);

        List<Claim> claims = new() { new(ClaimTypes.Name, user.Username) };
        IEnumerable<Role> roles = await _roleRepository.GetByUserIdAsync(user.Id);

        foreach (Role role in roles)
        {
            claims.Add(new(ClaimTypes.Role, role.Name));
        }

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(12),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}