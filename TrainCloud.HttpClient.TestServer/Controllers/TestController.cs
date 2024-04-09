using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TrainCloud.HttpClient.TestServer.Models;
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

    [HttpGet("Get/{id}")]
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(ResponseModel))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public IActionResult Get([FromRoute] Guid id)
    {
        var model = new ResponseModel()
        {
            CurrentUserId = CurrentUserId
        };

       return  Ok(model);
    }

    [HttpPost("Post")]
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status201Created, type: typeof(ResponseModel))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public IActionResult Post([FromBody] PostModel postModel)
    {
        var model = new ResponseModel()
        {
            CurrentUserId = CurrentUserId
        };

        return Created(model);
    }

    [HttpPatch("Patch/{id}")]
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(ResponseModel))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public IActionResult Patch([FromRoute] Guid id, [FromBody] PatchModel patchModel)
    {
        var model = new ResponseModel()
        {
            CurrentUserId = CurrentUserId
        };

        return Ok(model);
    }

    [HttpPut("Put/{id}")]
    [Authorize]
    [Consumes("application/json")]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(ResponseModel))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public IActionResult Put([FromRoute] Guid id, [FromBody] PutModel putModel)
    {
        var model = new ResponseModel()
        {
            CurrentUserId = CurrentUserId
        };

        return Ok(model);
    }

    [HttpDelete("Delete/{id}")]
    [Authorize]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        return NoContent();
    }
}
