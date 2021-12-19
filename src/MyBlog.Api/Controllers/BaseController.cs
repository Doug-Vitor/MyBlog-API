using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    protected ObjectResult DefaultInternalServerErrorResult() => StatusCode(500, new ErrorDTO("Ocorreu um erro em sua solicitação. Tente novamente."));
}
