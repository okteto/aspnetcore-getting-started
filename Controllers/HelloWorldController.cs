using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace helloworld.Controllers;

[ApiController]
public class HelloWorldController : ControllerBase
{
    // The message under the logo. Change it and save the file to see hot reload.
    // It's a property because hot reload can't change the value of a constant.
    public static string Message => "Built, deployed, and tested on Kubernetes.";

    // Get returns the message, the .NET version, and the pod that answers.
    [HttpGet("api/info")]
    public Info Get()
    {
        var pod = Environment.MachineName;
        return new Info(Message, RuntimeInformation.FrameworkDescription, pod);
    }
}

// Info is what the page shows.
public record Info(string Message, string Runtime, string Pod);
