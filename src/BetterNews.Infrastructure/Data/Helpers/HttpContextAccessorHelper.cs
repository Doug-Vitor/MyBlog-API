using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class HttpContextAccessorHelper
{
    private readonly IHttpContextAccessor _contextAcessor;

    public HttpContextAccessorHelper(IHttpContextAccessor contextAcessor) => _contextAcessor = contextAcessor;

    public int GetAuthenticatedUserId() => int.Parse(_contextAcessor.HttpContext.User
        .FindFirst(ClaimTypes.NameIdentifier).Value);
}
