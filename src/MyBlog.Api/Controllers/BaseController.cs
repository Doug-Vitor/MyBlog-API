using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    protected ObjectResult DefaultBadRequestResult() => StatusCode(400, new ErrorDTO(ModelState.Values.SelectMany(model => model.Errors.Select(error => error.ErrorMessage)).ToList()));
}
