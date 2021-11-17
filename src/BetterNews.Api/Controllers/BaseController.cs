using Microsoft.AspNetCore.Mvc;

namespace BetterNews.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
    }
}
