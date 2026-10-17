// Modern .NET, Chapter 7, Section 7.10
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5098");

builder.Services.Configure<CarrierOptions>(o =>
{
    o.BaseUrl = "http://localhost:5098/";
    o.Token = "demo-token";
});

builder.Services.AddHttpClient<ShipmentTracker>((sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<CarrierOptions>>().Value;
    client.BaseAddress = new Uri(options.BaseUrl);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.Token);
});

var app = builder.Build();

// Fake carrier endpoint, so the demo is self-contained.
app.MapGet("/track/{number}", (string number, HttpRequest req) =>
    req.Headers.Authorization == "Bearer demo-token"
        ? Results.Ok(new ShipmentStatus(number, "InTransit", DateTimeOffset.UtcNow.AddDays(2)))
        : Results.Unauthorized());

await app.StartAsync();

var tracker = app.Services.GetRequiredService<ShipmentTracker>();
var status = await tracker.TrackAsync("1Z999");
Console.WriteLine(status is null ? "Not found" : $"{status.TrackingNumber}: {status.State}, ETA {status.Eta:d}");

await app.StopAsync();

public sealed class CarrierOptions
{
    public string BaseUrl { get; set; } = "";
    public string Token { get; set; } = "";
}

public sealed record ShipmentStatus(string TrackingNumber, string State, DateTimeOffset Eta);

public sealed class ShipmentTracker(HttpClient http, ILogger<ShipmentTracker> logger)
{
    public async Task<ShipmentStatus?> TrackAsync(string trackingNumber, CancellationToken ct = default)
    {
        using var response = await http.GetAsync($"track/{Uri.EscapeDataString(trackingNumber)}", ct);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            logger.LogInformation("Tracking number {TrackingNumber} not found", trackingNumber);
            return null;
        }

        response.EnsureSuccessStatusCode();   // real failures surface as exceptions
        return await response.Content.ReadFromJsonAsync<ShipmentStatus>(ct);
    }
}
