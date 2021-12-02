using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class HttpContextAccessorHelper
{
    private readonly IHttpContextAccessor _contextAcessor;

    public HttpContextAccessorHelper(IHttpContextAccessor contextAcessor) => _contextAcessor = contextAcessor;

    internal async Task SignInUserAsync(List<Claim> claims)
    {
        if (_contextAcessor.HttpContext.User.Identity.IsAuthenticated) return;

        ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await _contextAcessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new(identity));
    }

    public async Task SignOutUserAsync()
    {
        if (_contextAcessor.HttpContext.User.Identity.IsAuthenticated)
            await _contextAcessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public int? GetAuthenticatedUserId()
    {
        if (_contextAcessor.HttpContext.User.Identity.IsAuthenticated)
            return int.Parse(_contextAcessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        return null;
    }
}
