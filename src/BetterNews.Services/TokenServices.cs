using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenServices : ITokenServices
{
    private readonly IRoleRepository _roleRepository;
    private readonly SecretsConfiguration _secretsConfiguration;

    public TokenServices(IRoleRepository roleRepository, IOptions<SecretsConfiguration> secretsConfiguration) =>
        (_roleRepository, _secretsConfiguration) = (roleRepository, secretsConfiguration.Value);

    public async Task<string> GenerateTokenAsync(User user)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.ASCII.GetBytes(_secretsConfiguration.Secret);

        List<Claim> claims = new() { new(ClaimTypes.Name, user.Username) };
        IEnumerable<Role> userRoles = await _roleRepository.GetByUserIdAsync(user.Id);

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