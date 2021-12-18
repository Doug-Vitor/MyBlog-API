using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenServices : ITokenServices
{
    private readonly IUserServices _userServices;
    private readonly SecretsConfiguration _secretsConfiguration;
    private readonly HttpContextAccessorHelper _contextAccessor;

    public TokenServices(IUserServices userServices, IOptions<SecretsConfiguration> secretsConfiguration, HttpContextAccessorHelper contextAccessor) =>
        (_userServices, _secretsConfiguration, _contextAccessor) = (userServices, secretsConfiguration.Value, contextAccessor);

    public async Task<string> GenerateTokenAsync(int? userId)
    {
        UserDTO userDto = await _userServices.GetByIdAsync(userId);
        List<Claim> claims = new()
        {
            new(ClaimTypes.Name, userDto.Username),
            new(ClaimTypes.Email, userDto.Email),
            new(ClaimTypes.NameIdentifier, userId.Value.ToString())
        };

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretsConfiguration.Secret)), SecurityAlgorithms.HmacSha256Signature)
        };

        await _contextAccessor.SignInUserAsync(claims);
        JwtSecurityTokenHandler tokenHandler = new();
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
}