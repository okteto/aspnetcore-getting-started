using System.Net;
using System.Net.Http.Json;
using helloworld.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace helloworld.Tests;

public class HelloWorldTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client = factory.CreateClient();

    [Fact]
    public async Task InfoReturnsTheMessageAndTheRuntime()
    {
        var response = await client.GetAsync("/api/info", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var info = await response.Content.ReadFromJsonAsync<Info>(TestContext.Current.CancellationToken);
        Assert.NotNull(info);
        Assert.Equal(HelloWorldController.Message, info.Message);
        Assert.StartsWith(".NET ", info.Runtime);
    }

    [Fact]
    public async Task PageShowsTheMessage()
    {
        var response = await client.GetAsync("/", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var page = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);
        Assert.Contains("id=\"message\"", page);
    }
}
