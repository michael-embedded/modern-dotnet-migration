// Modern .NET, Chapter 13, Section 13.9
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDiscovery();
builder.Services.ConfigureHttpClientDefaults(http => http.AddServiceDiscovery());
builder.Services.AddHttpClient("inventory", c => c.BaseAddress = new Uri("http://inventory"));

// Configuration-based resolution: logical name -> real address.
builder.Configuration["Services:inventory:http:0"] = "http://localhost:5200";

var app = builder.Build();
app.Urls.Add("http://localhost:5200");

app.MapGet("/stock", () => new { sku = "ABC", quantity = 42 });

app.MapGet("/", async (IHttpClientFactory factory) =>
{
    var client = factory.CreateClient("inventory");         // knows only the NAME "inventory"
    return await client.GetStringAsync("/stock");
});

app.Run();
