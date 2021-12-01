using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class HttpContextAccessorHelper
{
    private readonly IHttpContextAccessor _contextAcessor;

    public HttpContextAccessorHelper(IHttpContextAccessor contextAcessor) => _contextAcessor = contextAcessor;

    internal async Task SignInUserAsync(List<Claim> claims)
    {
        if (_contextAcessor.HttpContext.User.Identity.IsAuthenticated) return;

        ClaimsIdentity identity = new(claims);
        await _contextAcessor.HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new(identity));
    }

    public async Task SignOutUserAsync()
    {
        if (_contextAcessor.HttpContext.User.Identity.IsAuthenticated)
            await _contextAcessor.HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
    }

    public int? GetAuthenticatedUserId()
    {
        if (_contextAcessor.HttpContext.User.Identity.IsAuthenticated)
            return int.Parse(_contextAcessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        return null;
    }
}
