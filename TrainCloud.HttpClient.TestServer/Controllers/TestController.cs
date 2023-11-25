using Microsoft.AspNetCore.Mvc;

namespace TrainCloud.HttpClient.TestServer.Controllers;
[ApiController]
[Route("/")]
public class TestController : ControllerBase
{
    public TestController()
    {

    }

    [HttpGet()]
    public IActionResult Get()
    {
       return  Ok();
    }
}
