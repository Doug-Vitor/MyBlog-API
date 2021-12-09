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
        UserDTO userViewModel = await _userServices.GetByIdAsync(userId);

        JwtSecurityTokenHandler tokenHandler = new();
        byte[] key = Encoding.ASCII.GetBytes(_secretsConfiguration.Secret);

        List<Claim> claims = new()
        {
            new(ClaimTypes.Name, userViewModel.Username),
            new(ClaimTypes.Email, userViewModel.Email),
            new(ClaimTypes.NameIdentifier, userId.Value.ToString())
        };

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        await _contextAccessor.SignInUserAsync(claims);
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
}