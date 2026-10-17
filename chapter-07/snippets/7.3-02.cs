// Modern .NET, Chapter 7, Section 7.3
using System.Net.Http.Json;

var attempts = 0;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5099");

builder.Services
    .AddHttpClient<InventoryClient>(client =>
    {
        client.BaseAddress = new Uri("http://localhost:5099/");
        client.Timeout = Timeout.InfiniteTimeSpan;   // the resilience pipeline owns timeouts
    })
    .AddStandardResilienceHandler(options =>
    {
        options.Retry.MaxRetryAttempts = 3;
        options.Retry.Delay = TimeSpan.FromMilliseconds(100);   // short, for the demo
    });

var app = builder.Build();

// A flaky endpoint: fails twice, then succeeds.
app.MapGet("/stock/{sku}", (string sku) =>
{
    var n = Interlocked.Increment(ref attempts);
    if (n <= 2)
    {
        Console.WriteLine($"  server: attempt {n} -> 503");
        return Results.StatusCode(503);
    }
    Console.WriteLine($"  server: attempt {n} -> 200");
    return Results.Ok(new StockLevel(sku, 42));
});

await app.StartAsync();

var client = app.Services.GetRequiredService<InventoryClient>();
var level = await client.GetStockAsync("ABC-123");
Console.WriteLine($"Client received: {level}");

await app.StopAsync();

public sealed record StockLevel(string Sku, int Quantity);

public sealed class InventoryClient(HttpClient http)
{
    public async Task<StockLevel?> GetStockAsync(string sku, CancellationToken ct = default) =>
        await http.GetFromJsonAsync<StockLevel>($"stock/{sku}", ct);
}
