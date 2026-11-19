using Microsoft.AspNetCore.Mvc;

namespace helloworld.Controllers;

[ApiController]
[Route("")]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        var message = "Hello world!";
        return message;
    }
}
