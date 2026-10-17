// Modern .NET, Chapter 11, Section 11.4
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Store.Api.Tests;

public class PriceEndpointTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private record PriceResponse(string Sku, decimal Price);

    [Fact]
    public async Task Known_Sku_Returns_Price()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/prices/pen");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<PriceResponse>();
        Assert.Equal(1.50m, body!.Price);
    }

    [Fact]
    public async Task Unknown_Sku_Returns_404()
    {
        var response = await factory.CreateClient().GetAsync("/prices/nope");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Service_Can_Be_Replaced_In_Tests()
    {
        var custom = factory.WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<IPriceService>();
                services.AddSingleton<IPriceService>(new FixedPrice(99m));
            }));

        var body = await custom.CreateClient().GetFromJsonAsync<PriceResponse>("/prices/anything");

        Assert.Equal(99m, body!.Price);
    }

    private sealed class FixedPrice(decimal price) : IPriceService { public decimal? Find(string sku) => price; }
}
