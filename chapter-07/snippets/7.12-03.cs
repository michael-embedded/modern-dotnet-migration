// Modern .NET, Chapter 7, Section 7.12
using System.Net.Http.Json;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5097");

builder.Services
    .AddHttpClient<IRateClient, RateClient>(c => c.BaseAddress = new Uri("http://localhost:5097/"))
    .AddStandardResilienceHandler();

var app = builder.Build();

app.MapGet("/rates/{from}/{to}", (string from, string to) =>
    (from.ToUpperInvariant(), to.ToUpperInvariant()) switch
    {
        ("USD", "EUR") => Results.Ok(new RateResponse(0.92m)),
        ("EUR", "USD") => Results.Ok(new RateResponse(1.09m)),
        _              => Results.NotFound()
    });

await app.StartAsync();

// Behavioral comparison: modern vs legacy on the same inputs.
var modern = app.Services.GetRequiredService<IRateClient>();
var legacy = new LegacyRateClient("http://localhost:5097/");

foreach (var (amount, from, to) in new[] { (100m, "USD", "EUR"), (59.99m, "EUR", "USD") })
{
    var m = await modern.ConvertAsync(amount, from, to);
    var l = legacy.Convert(amount, from, to);
    Console.WriteLine($"{amount} {from}->{to}: modern={m} legacy={l} match={m == l}");
}

var missing = await modern.ConvertAsync(10m, "USD", "XXX");
Console.WriteLine($"Unknown pair returns: {(missing is null ? "null (handled)" : missing.ToString())}");

await app.StopAsync();

public sealed record RateResponse(decimal Rate);

public interface IRateClient
{
    Task<decimal?> ConvertAsync(decimal amount, string from, string to, CancellationToken ct = default);
}

public sealed class RateClient(HttpClient http) : IRateClient
{
    public async Task<decimal?> ConvertAsync(decimal amount, string from, string to, CancellationToken ct = default)
    {
        using var response = await http.GetAsync(
            $"rates/{Uri.EscapeDataString(from)}/{Uri.EscapeDataString(to)}", ct);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<RateResponse>(ct);
        return body is null ? null : Math.Round(amount * body.Rate, 2);
    }
}
