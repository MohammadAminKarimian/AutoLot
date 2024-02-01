using Microsoft.AspNetCore.Mvc;

namespace AutoLot.Api.Controllers.Test;

[AllowAnonymous]
[ApiVersion("1.0")]
[ApiController]
[Route("api/test/[Controller]")]
public class FilterTestController : ControllerBase
{
    [HttpGet]
    [TypeFilter(typeof(LoggingResponseHeaderFilter), Arguments = null )]
    //[ServiceFilter(typeof(LoggingResponseHeaderFilter))]
    [ApiVersion("1.0")]
    public IActionResult Get_01()
    {
        return Content($"- {nameof(CarsController)}.{nameof(Get_01)}");
    }
}
