// Modern .NET, Chapter 8, Section 8.12
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ShopMvc.Tests;

public class ProductsTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

    [Fact]
    public async Task Index_ListsSeedProduct()
    {
        var html = await _client.GetStringAsync("/Products");
        Assert.Contains("Keyboard", html);
    }

    [Fact]
    public async Task Search_FiltersCaseInsensitively()
    {
        var html = await _client.GetStringAsync("/Products?q=KEY");
        Assert.Contains("Keyboard", html);
    }

    [Fact]
    public async Task Details_UnknownId_Returns404()
    {
        var response = await _client.GetAsync("/Products/Details/999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Details_KnownId_Returns200()
    {
        var response = await _client.GetAsync("/Products/Details/1");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
