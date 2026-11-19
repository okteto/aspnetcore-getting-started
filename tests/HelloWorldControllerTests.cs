using helloworld.Controllers;

namespace helloworld.Tests;

public class HelloWorldControllerTests
{
    [Fact]
    public void GetReturnsHelloWorld()
    {
        var controller = new HelloWorldController();

        Assert.Equal("Hello world!", controller.Get());
    }
}
