using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrainCloud.Microservices.Core.Controllers;

namespace TrainCloud.HttpClient.TestServer.Controllers;

[ApiController]
[Route("/")]
public class TestController : AbstractController<TestController>
{
    public TestController(IWebHostEnvironment webHostEnvironment,
                          IHttpContextAccessor httpContextAccessor,
                          IConfiguration configuration,
                          ILogger<TestController> logger)
        : base(webHostEnvironment, httpContextAccessor, configuration, logger)
    {

    }


    [HttpGet("Get")]
    [Authorize()]
    [Consumes("application/json")]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(object))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public IActionResult Get()
    {
       return  Ok();
    }

    [HttpPost()]
    public IActionResult Post()
    {
        return Ok();
    }

    [HttpPatch()]
    public IActionResult Patch()
    {
        return Ok();
    }

    [HttpPut()]
    public IActionResult Put()
    {
        return Ok();
    }

    [HttpDelete()]
    public IActionResult Delete()
    {
        return Ok();
    }
}
